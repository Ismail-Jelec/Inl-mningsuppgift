public class Shoe
{
    public const int DefaultDeckCount = 6;
    private const int CardsPerDeck = 52;

    private readonly Random _random;
    private readonly int _deckCount;
    private readonly List<Card> _cards = new();
    private int _cutCardPosition;

    public Shoe(Random random, int deckCount)
    {
        _random = random;
        _deckCount = deckCount;
        Reset();
    }

    public int Remaining => _cards.Count;

    public double DecksRemaining => (double)Remaining / CardsPerDeck;


    public bool NeedsReshuffle => Remaining <= _cutCardPosition;

    public void Reset()
    {
        _cards.Clear();

        for (int i = 0; i < _deckCount; i++)
        {
            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                foreach (Rank rank in Enum.GetValues<Rank>())
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }
        
    }
}