public class Round
{
    private const int InitialCardsPerParticipant = 2;

    private const int Blackjack = 21;
    private readonly Shoe _shoe;
    private readonly Dealer _dealer;
    private readonly IReadOnlyList<Player> _players;

    private readonly RuleSet _rules;

// Tar emot skon, givaren och listan med spelare.
    public Round(Shoe shoe, Dealer dealer, IReadOnlyList<Player> players, RuleSet rules)
    {
        _shoe = shoe;
        _dealer = dealer;
        _players = players;
        _rules = rules;
    }

// Delar ut två kort till varje spelare och två kort till dealern.
    public void DealInitialCards()
    {
        // Rensar spelarnas händer inför den nya rundan.
        foreach (Player player in _players)
        {
        player.ClearHand();
        }

        _dealer.ClearHand();

        // Delar ut ett kort i taget tills spelarna har 2 var.
        for (int i = 0; i < InitialCardsPerParticipant; i++)
        {
            foreach (Player player in _players)
            {
            player.ReceiveCard(_shoe.Draw());
            }

            _dealer.ReceiveCard(_shoe.Draw());
        }
    }

// Spelaren får köpa tills den stannar, spräcker eller har 21.
    private void PlayPlayerTurn(Player player)
    {
        while (!player.Hand.IsBust &&player.Hand.Value < Blackjack)
        {
            Move move = player.Decide(_dealer.UpCard);

            if (move == Move.Stand)
            {
                break;
            }
            player.ReceiveCard(_shoe.Draw());
        }
    }

// Rundan spelas alltid färdigt, även om alla spelare har spräckt/bustat.
    private void PlayDealerTurn()
    {
        _dealer.RevealHoleCard();
 
        while (_dealer.ShouldHit())
        {
            _dealer.ReceiveCard(_shoe.Draw());
        }

    }
    private Dictionary<Player, Outcome> ResolveOutcomes()
    {
        var results = new Dictionary<Player, Outcome>();
 
        foreach (Player player in _players)
        {
            results[player] = OutcomeResolver.Resolve(player.Hand, _dealer.Hand, _rules);
        }
 
        return results;
    }
        

// Varje spelare satsar innan korten delas ut.
    private void TakeBets()
    {
        foreach (Player player in _players)
        {
            player.Bankroll.PlaceBet(player.ChooseBet());
        }
    }

// Betalar ut insatserna efter att utfallen är avgjorda.
    private void SettleBets(Dictionary<Player, Outcome> results)
    {
        foreach (var (player, outcome) in results)
        {
            player.Bankroll.Settle(outcome);
        }
    }

    public Dictionary<Player, Outcome> Play()
    {
        TakeBets();
        DealInitialCards();

        foreach (Player player in _players)
        {
            PlayPlayerTurn(player);
        }

        PlayDealerTurn();

        Dictionary<Player, Outcome> results = ResolveOutcomes();
        SettleBets(results);
        return results;
    }


}