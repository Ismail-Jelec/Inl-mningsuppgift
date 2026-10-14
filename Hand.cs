// Värdet räknas om varje gång det efterfrågas och sparas aldrig, eftersom
// ett ess kan gå från 11 till 1 när handen får fler kort.
public class Hand
{
    private const int Blackjack = 21;
    private const int SoftAceBonus = 10;

    private readonly List<Card> _cards = new();

    public IReadOnlyList<Card> Cards => _cards;

    public void Add(Card card) => _cards.Add(card);

    public void Clear() => _cards.Clear();

    // Alla ess räknas först som 1. Om det inte spräcker handen räknas ett av
    // dem som 11. Fler än ett ess kan aldrig räknas som 11, då blir det minst 22.
    public int Value => HardValue + (CanCountAceAsEleven ? SoftAceBonus : 0);

    // Sant om ett ess just nu räknas som 11.
    public bool IsSoft => CanCountAceAsEleven;

    public bool IsBust => Value > Blackjack;

    // Naturlig tjugoett: 21 på exakt två kort.
    public bool IsNatural => _cards.Count == 2 && Value == Blackjack;

    private int HardValue => _cards.Sum(card => CardValue(card.Rank));

    private bool CanCountAceAsEleven =>
        _cards.Any(card => card.Rank == Rank.Ace) && HardValue + SoftAceBonus <= Blackjack;

    // Ess räknas här som 1, klädda kort som 10.
    private static int CardValue(Rank rank) => Math.Min((int)rank, 10);
}
