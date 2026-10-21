// Köper så länge handen är under 17. Satsar alltid samma belopp.
public class SimpleStrategy : IPlayerStrategy
{
    private const int StandAt = 17;
    private const int FixedBet = 10;

    public Move Decide(Hand hand, Card dealerUpcard) =>
        hand.Value < StandAt ? Move.Hit : Move.Stand;

    public int ChooseBet(int chips) => Math.Min(FixedBet, chips);
}
