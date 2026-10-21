// Följer grundstrategin för ett spel utan dubbling och delning. 
// Där givaren stannar på mjuk 17.
public class BasicStrategy : IPlayerStrategy
{
    private const int FixedBet = 10;

    // Varje tecken är givarens öppna kort: 2, 3, 4, 5, 6, 7, 8, 9, 10, A.
    // S = stanna, H = köpa
    private static readonly Dictionary<int, string> HardTable = new()
    {
        [12] = "HHSSSHHHHH",
        [13] = "SSSSSHHHHH",
        [14] = "SSSSSHHHHH",
        [15] = "SSSSSHHHHH",
        [16] = "SSSSSHHHHH",
    };
    private static readonly Dictionary<int, string> SoftTable = new()
    {
        [18] = "SSSSSSSHHS",
    };

    public Move Decide(Hand hand, Card dealerUpcard)
    {
        // 1. välj tabell.
        var table = hand.IsSoft ? SoftTable : HardTable;

        // 2. värden under tabellen köper alltid, värden över stannar alltid.
        int lowest = table.Keys.Min();
        int highest = table.Keys.Max();
        if (hand.Value < lowest) return Move.Hit;
        if (hand.Value > highest) return Move.Stand;

        // 3. Välj kolumn. Givaren 2 blir index 0, esset (11), blir index 9.
        int column = UpcardValue(dealerUpcard) - 2;

        // 4. Läs tecknet.
        return table[hand.Value][column] == 'S' ? Move.Stand : Move.Hit;
    }

    public int ChooseBet(int chips) => Math.Min(FixedBet, chips);

    // Esset räknas som 11 här, så att den hamnar sist i tabellen.
    private static int UpcardValue(Card card) =>
        card.Rank == Rank.Ace ? 11 : Math.Min((int)card.Rank, 10);

}