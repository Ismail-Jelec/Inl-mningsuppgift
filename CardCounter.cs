// Hi-Lo-räknare. Den observerar bara korten som visats och ändrar ingenting i spelet.
public class CardCounter
{
    private const double FavorableTrueCount = 2;
    private const double UnfavorableTrueCount = -1;

    // Summan av räknevärdet för alla kort som setts sedan skon blandades.
    public int RunningCount { get; private set; }

    public void Observe(IEnumerable<Card> cards)
    {
        foreach (Card card in cards)
        {
            RunningCount += CountValue(card.Rank);
        }
    }

    // Anropas när skon blandas om, eftersom alla kort då är tillbaka.
    public void Reset() => RunningCount = 0;

    // Den löpande räkningen delad med antalet kvarvarande lekar.
    public double TrueCount(double decksRemaining)
    {
        return decksRemaining > 0 ? RunningCount / decksRemaining : 0;
    }

    public CountTrend Trend(double decksRemaining)
    {
        double trueCount = TrueCount(decksRemaining);

        if (trueCount >= FavorableTrueCount) return CountTrend.Favorable;
        if (trueCount <= UnfavorableTrueCount) return CountTrend.Unfavorable;
        return CountTrend.Neutral;
    }

    // 2–6 ger +1, 7–9 ger 0, tiokort och ess ger −1.
    private static int CountValue(Rank rank) => rank switch
    {
        >= Rank.Two and <= Rank.Six => 1,
        >= Rank.Seven and <= Rank.Nine => 0,
        _ => -1
    };
}
