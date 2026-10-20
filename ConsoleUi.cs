using System.Text;

// All in- och utmatning. Ingen annan klass använder Console.
public class ConsoleUi
{
    private const int MaxNameLength = 20;

    private readonly CardRenderer _renderer = new();
    public ConsoleUi()
    {
        // Utan detta visas ♥♦♣♠ som frågetecken i Windows-konsolen.
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void ShowMessage(string text) => Console.WriteLine(text);

    public int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Skriv ett tal mellan {min} och {max}.");
        }
    }

    // Console.ReadLine ger null om inmatningen tar slut (t.ex. Ctrl+Z).
    // Resultaten sparas efter varje runda, så programmet kan avslutas direkt.
    private static string ReadLine()
    {
        string? line = Console.ReadLine();
        if (line == null)
        {
            Environment.Exit(0);
        }
        return line.Trim();
    }

    public string AskName()
    {
        while (true)
        {
            Console.Write("Namn: ");
            string name = ReadLine();
            if (name.Length > 0 && name.Length <= MaxNameLength)
            {
                return name;
            }
            Console.WriteLine($"Namnet måste vara 1–{MaxNameLength} tecken.");
        }
    }

    // Visar spelarens hand och givarens öppna kort, aldrig hålkortet.
    // Frågar tills svaret är k eller s.
    public Move AskMove(string name, Hand hand, Card dealerUpCard)
    {
        Console.WriteLine();
        Console.WriteLine("Givaren:");
        DrawCards(new Card?[] { dealerUpCard, null});
        Console.WriteLine("");
        Console.WriteLine("Du:");
        DrawCards(hand.Cards);
        Console.WriteLine($"Givaren visar: {FormatCard(dealerUpCard)} [dolt]");
        Console.WriteLine($"{name}: {FormatHand(hand)}");

        while (true)
        {
            Console.Write("(k)öpa eller (s)tanna? ");
            string answer = ReadLine().ToLower();
            if (answer == "k") return Move.Hit;
            if (answer == "s") return Move.Stand;
            Console.WriteLine("Skriv k för att köpa eller s för att stanna.");
        }
    }

    public void ShowRoundResult(Dealer dealer, Dictionary<Player, Outcome> results)
    {
        Console.WriteLine();
        Console.WriteLine("--- Resultat ---");
        Console.WriteLine($"Givaren: {FormatHand(dealer.Hand)}");
        DrawCards(dealer.Hand.Cards);
        
        foreach (var (player, outcome) in results)
        {
            Console.WriteLine($"{player.Name}: {FormatHand(player.Hand)} – {FormatOutcome(outcome)}");
            DrawCards(player.Hand.Cards);
        }
    }

    public void ShowStats(IEnumerable<Player> players)
    {
        Console.WriteLine();
        Console.WriteLine($"{"Namn",-20} {"Vinster",7}  {"Förluster",9}  {"Lika",4}");
        foreach (Player p in players)
        {
            Console.WriteLine($"{p.Name,-20} {p.Wins,7}  {p.Losses,9}  {p.Pushes,4}");
        }
    }

    // Till exempel "♥A ♠6 (mjuk 17)".
    private static string FormatHand(Hand hand)
    {
        string cards = string.Join(" ", hand.Cards.Select(FormatCard));
        string value = hand.IsBust ? $"{hand.Value}, spräckt"
            : hand.IsNatural ? "naturlig tjugoett"
            : hand.IsSoft ? $"mjuk {hand.Value}"
            : hand.Value.ToString();
        return $"{cards} ({value})";
    }

    private static string FormatCard(Card card) => SuitSymbol(card.Suit) + RankText(card.Rank);

    private static string SuitSymbol(Suit suit) => suit switch
    {
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        _ => "♠"
    };

    private static string RankText(Rank rank) => rank switch
    {
        Rank.Ace => "A",
        Rank.Jack => "Kn",
        Rank.Queen => "D",
        Rank.King => "K",
        _ => ((int)rank).ToString()
    };

    private static string FormatOutcome(Outcome outcome) => outcome switch
    {
        Outcome.Win => "Vinst",
        Outcome.Loss => "Förlust",
        _ => "Lika"
    };

    private void DrawCards(IEnumerable<Card?> cards)
    {
        var rendered = cards
            .Select(card => card == null
                ? (Lines: _renderer.RenderBack(), Color: (ConsoleColor?)null)
                : (Lines: _renderer.Render(card), Color: ColorFor(card.Suit)))
            .ToList();

        for (int row = 0; row < rendered[0]. Lines.Length; row++)
        {
            foreach (var (lines, color) in rendered)
            {
                if (color != null)
                {
                    Console.ForegroundColor = color.Value;
                }
                Console.Write(lines[row] + " ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
        private static ConsoleColor? ColorFor(Suit suit) => suit is Suit.Hearts or Suit.Diamonds ? ConsoleColor.Red : null;
}
