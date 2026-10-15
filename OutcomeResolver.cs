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

        if (player.IsNatural && !dealer.IsNatural)
        {
            return Outcome.Win;
        }

        if (!player.IsNatural && dealer.IsNatural)
        {
            return Outcome.Loss;
        }

        if (player.Value > dealer.Value)
        {
            return Outcome.Win;
        }

        if (dealer.Value > player.Value)
        {
            return Outcome.Loss;
        }

        return Outcome.Draw;
    }
}