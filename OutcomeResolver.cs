public static class OutcomeResolver
{
    public static Outcome Resolve(Hand player, Hand dealer)
    {
        if (player.IsBust)
        {
            return Outcome.Loss;
        }

        if (dealer.IsBust)
        {
            return Outcome.Win;
        }

        if (player.IsNatural && dealer.IsNatural)
        {
            return Outcome.Draw;
        }

        return Outcome.Draw;
    }
}
