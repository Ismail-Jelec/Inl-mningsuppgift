public static class OutcomeResolver
{
    public static Outcome Resolve(Hand player, Hand dealer, RuleSet rules)
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

        if (player.IsNatural)
        {
            // Spelaren är naturlig men givaren inte. Har givaren ändå 21
            // måste det vara på tre kort eller fler.
            if (!rules.NaturalBeatsThreeCard21 && dealer.Value == 21)
            {
                return Outcome.Draw;
            }
            return Outcome.Blackjack;
        }

        if (dealer.IsBust)
        {
            return Outcome.Win;
        }

        if (dealer.IsNatural)
        {
            // Givaren är naturlig men spelaren inte. Har spelaren ändå 21
            // måste det vara på tre kort eller fler.
            if (!rules.NaturalBeatsThreeCard21 && player.Value == 21)
            {
                return Outcome.Draw;
            }
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