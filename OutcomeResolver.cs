public static class OutcomeResolver
{
    public static Outcome Resolve(Hand player, Hand dealer)
    {
        // Spräcker spelaren är det förlust, även om givaren också spräcker.
        if (player.IsBust)
        {
            return Outcome.Loss;
        }

        if (player.IsNatural && dealer.IsNatural)
        {
            return Outcome.Draw;
        }

        // Naturlig tjugoett måste kollas före givarens spräckta hand,
        // annars blir det Win (1:1) i stället för Blackjack (3:2).
        if (player.IsNatural)
        {
            return Outcome.Blackjack;
        }

        if (dealer.IsBust)
        {
            return Outcome.Win;
        }

        if (dealer.IsNatural)
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