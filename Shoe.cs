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
        
        Shuffle();

        int quarter = _cards.Count / 4;
        _cutCardPosition = _random.Next(quarter / 2, quarter);
    }

    public Card Draw()
    {
        Card card = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }

    private void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }
}