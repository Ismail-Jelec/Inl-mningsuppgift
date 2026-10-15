public class Round
{
    private const int InitialCardsPerParticipant = 2;
    private readonly Deck _deck;
    private readonly Dealer _dealer;
    private readonly IReadOnlyList<Player> _players;

// Tar emot kortleken, givaren och listan med spelare.
    public Round(Deck deck, Dealer dealer, IReadOnlyList<Player> players)
    {
    _deck = deck;
    _dealer = dealer;
    _players = players;
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
            player.ReceiveCard(_deck.Draw());
            }

            _dealer.ReceiveCard(_deck.Draw());
        }
    }
}