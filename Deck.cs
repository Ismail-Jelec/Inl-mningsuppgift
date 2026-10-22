public class Deck
{
  

    private readonly Random _random;
    private readonly RuleSet _rules;
    private readonly List<Card> _cards = new();

    // Random skapas en gång i Program och delas av hela programmet. En ny
    // Random vid varje blandning ger samma lek om och om igen.
    public Deck(Random random, RuleSet rules)
    {
        _random = random;
        _rules = rules;
        Reset();
    }

    public int Remaining => _cards.Count;

    // Kollas inför varje runda.
    public bool NeedsReshuffle => Remaining < _rules.ReshuffleThreshold;

    // Bygger en ny lek med 52 kort och blandar den.
    public void Reset()
    {
        _cards.Clear();

        for (int i = 0; i < _rules.DeckCount; i++)
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
    }

    public Card Draw()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("Kortleken är tom.");
        }

        Card card = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }

    // Fisher–Yates: gå baklänges och byt plats på varje kort med ett
    // slumpmässigt kort bland dem som ligger före, eller med sig självt.
    private void Shuffle()
    {
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }
}
