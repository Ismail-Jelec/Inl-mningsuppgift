public class Game
{
    public const int MaxPlayers = 5;

    private readonly ConsoleUi _ui;
    private readonly GameStorage _storage;
    private readonly Shoe _shoe;
    private readonly CardCounter _counter = new();

    private readonly RuleSet _rules;
    private readonly Dealer _dealer;

    private readonly List<Player> _knownPlayers = new();

    private readonly List<Player> _table = new();
    private int _computerCount = 0; 

    public Game(ConsoleUi ui, GameStorage storage, Shoe shoe, RuleSet rules)
    {
        _ui = ui;
        _storage = storage;
        _shoe = shoe;
        _rules = rules;
        _dealer = new Dealer(rules);
    }

    public void Run()
    {
        LoadPlayers();

        while (true)
        {
            ShowMenu();
            int choice = _ui.ReadInt("Välj: ", 0, 5);

            switch (choice)
            {
                case 1: AddPlayer(); break;
                case 2: RemovePlayer(); break;
                case 3: PlayRound(); break;
                case 4: _ui.ShowStats(_knownPlayers); break;
                case 5: AddComputerPlayer(); break;
                case 0: SavePlayers(); return;
            }
        }
        
    }

    private void ShowMenu()
    {
        string seated = _table.Count == 0 ? "inga" : string.Join(", ", _table.Select(p => p.Name));

        _ui.ShowMessage("");
        _ui.ShowMessage($"Vid bordet ({_table.Count}/{MaxPlayers}): {seated}");
        _ui.ShowMessage("1. Lägg till spelare");
        _ui.ShowMessage("2. Ta bort spelare");
        _ui.ShowMessage("3. Spela en runda");
        _ui.ShowMessage("4. Visa resultat");
        _ui.ShowMessage("5. Lägg till datorspelare");
        _ui.ShowMessage("0. Avsluta");
    }

    private void LoadPlayers()
    {
        foreach (PlayerRecord record in _storage.Load())
        {
            _knownPlayers.Add(CreatePlayer(record.Name, record.Wins, record.Losses, record.Pushes, record.Chips));
        }

        if (_storage.LastError != null)
        {
            _ui.ShowMessage(_storage.LastError);
        }
    }

    private void AddPlayer()
    {
        if (_table.Count >= MaxPlayers)
        {
            _ui.ShowMessage($"Bordet är fullt, max {MaxPlayers} spelare.");
            return;
        }

        string name = _ui.AskName();

        if (FindByName(_table, name) != null)
        {
            _ui.ShowMessage($"{name} sitter redan vid bordet.");
            return;
        }

        // Finns namnet i filen tas den spelaren fram, med sin gamla statistik.
        Player? player = FindByName(_knownPlayers, name);

        if (player == null)
        {
            player = CreatePlayer(name, 0, 0, 0, Bankroll.StartingChips);
            _knownPlayers.Add(player);
            _ui.ShowMessage($"Ny spelare: {name}");
        }
        else
        {
            _ui.ShowMessage($"Välkommen tillbaka, {player.Name}!");
        }

        // En spelare som gick ut med noll marker får inte sätta sig igen.
        if (player.Bankroll.IsEmpty)
        {
            _ui.ShowMessage($"{player.Name} har inga marker kvar och kan inte spela.");
            return;
        }

        _table.Add(player);
    }

    private void AddComputerPlayer()
    {
        if (_table.Count >= MaxPlayers)
        {
            _ui.ShowMessage($"Bordet är fullt, Max {MaxPlayers} spelare.");
            return;
        }

        _ui.ShowMessage("1. Enkel (Köper under 17)");
        _ui.ShowMessage("2. Grundstrategi");
        int type = _ui.ReadInt("Välj Strategi: ", 1,2);

        IPlayerStrategy strategy = type == 1 ? new SimpleStrategy() : new BasicStrategy();
        string label = type == 1 ? "Dator-enkel" : "Dator-grund";
        _computerCount++;

        // Datorspelare sparas inte i json-filen, Därför läggs de inte i _knownPlayers.
        // Filen vet inte vilken strategi spelaren hade, så nästa gång spelet startar 
        // skulle datorn bli en vanlig människa.
        _table.Add(new Player($"{label} {_computerCount}", strategy, 0, 0, 0, Bankroll.StartingChips));
    }
    private void RemovePlayer()
    {
        if (_table.Count == 0)
        {
            _ui.ShowMessage("Det sitter ingen vid bordet.");
            return;
        }

        for (int i = 0; i < _table.Count; i++)
        {
            _ui.ShowMessage($"{i + 1}. {_table[i].Name}");
        }

        int number = _ui.ReadInt("Vem ska lämna bordet? ", 1, _table.Count);
        Player player = _table[number - 1];
        _table.RemoveAt(number - 1);
        _ui.ShowMessage($"{player.Name} lämnade bordet.");
    }

    private void PlayRound()
    {
        if (_table.Count == 0)
        {
            _ui.ShowMessage("Lägg till minst en spelare först.");
            return;
        }

        // Kollas före rundan, inte under, så att en runda aldrig byter sko halvvägs.
        if (_shoe.NeedsReshuffle)
        {
            _ui.ShowMessage(_shoe.CutCardReached
                ? "Skiljekortet har kommit fram. Skon blandas om."
                : $"Färre än {_rules.ReshuffleThreshold} kort kvar. Skon blandas om.");
            _shoe.Reset();
            _counter.Reset();
        }

        _ui.ShowCount(_counter.RunningCount, _counter.TrueCount(_shoe.DecksRemaining), _counter.Trend(_shoe.DecksRemaining));

        var round = new Round(_shoe, _dealer, _table, _rules);
        Dictionary<Player, Outcome> results = round.Play();

        // Räknaren får se korten först när rundan är slut och hålkortet är vänt.
        // Den läser bara händerna och ändrar ingenting i spelet.
        _counter.Observe(_dealer.Hand.Cards.Concat(_table.SelectMany(p => p.Hand.Cards)));

        foreach (var (player, outcome) in results)
        {
            player.RecordOutcome(outcome);
        }

        _ui.ShowRoundResult(_dealer, results);

        // ToList behövs eftersom man inte kan ta bort ur _table medan man loopar över den.
        foreach (Player player in _table.Where(p => p.Bankroll.IsEmpty).ToList())
        {
            _table.Remove(player);
            _ui.ShowMessage($"{player.Name} har slut på marker och lämnar bordet.");
        }

        SavePlayers();
    }

    private void SavePlayers()
    {
        var records = _knownPlayers.Select(p => new PlayerRecord(p.Name, p.Wins, p.Losses, p.Pushes, p.Bankroll.Chips));

        if (!_storage.Save(records))
        {
            _ui.ShowMessage(_storage.LastError!);
        }
    }

    private Player CreatePlayer(string name, int wins, int losses, int pushes, int chips)
    {
        return new Player(name, new HumanStrategy(_ui, name), wins, losses, pushes, chips);
    }

    // Namn jämförs utan hänsyn till stora och små bokstäver, så "anna" och "Anna" är samma spelare.
    private static Player? FindByName(IEnumerable<Player> players, string name)
    {
        return players.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}