// Skon: flera lekar blandade till en, med ett skiljekort i den bakre fjärdedelen.
// Antalet lekar och den lägsta gränsen för omblandning kommer från husreglerna.
public class Shoe
{
    private const int CardsPerDeck = 52;

    private readonly Random _random;
    private readonly RuleSet _rules;
    private readonly List<Card> _cards = new();

    // Antal kort som ligger kvar bakom skiljekortet.
    private int _cutCardPosition;

    // Random skapas en gång i Program och delas av hela programmet. En ny
    // Random vid varje blandning ger samma sko om och om igen.
    public Shoe(Random random, RuleSet rules)
    {
        _random = random;
        _rules = rules;
        Reset();
    }

    public int Remaining => _cards.Count;

    // Används av korträknaren för att räkna fram den sanna räkningen.
    public double DecksRemaining => (double)Remaining / CardsPerDeck;

    // Sant när skiljekortet har dragits. Rundan spelas färdigt först.
    public bool CutCardReached => Remaining <= _cutCardPosition;

    // Kollas inför varje runda. Gränsen i husreglerna är ett golv, så att skon
    // aldrig kan ta slut mitt i en runda även om skiljekortet ligger långt bak.
    public bool NeedsReshuffle => CutCardReached || Remaining < _rules.ReshuffleThreshold;

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

        // Skiljekortet läggs på en slumpmässig plats i den bakre fjärdedelen.
        int quarter = _cards.Count / 4;
        _cutCardPosition = _random.Next(1, quarter + 1);
    }

    public Card Draw()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("Skon är tom.");
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
