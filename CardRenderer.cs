public class CardRenderer
{
    public string[] Render(Card card)
    {
        string rank = RankText(card.Rank);
        string suit = SuitSymbol(card.Suit);

        return new[]
        {
            "┌───────┐",
           $"│{rank, -2}     │",
            "│       │",
           $"│   {suit}   │",
            "│       │",
           $"│     {rank, 2}│",
            "└───────┘"
        };

    }



    public string[] RenderBack()
    {
        return new[]
        {
            "┌───────┐",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "│▒▒▒▒▒▒▒│",
            "└───────┘"
        };
    }

    // Används även av ConsoleUi för textvisningen, så symbolerna bara finns på ett ställe.
    public static string SuitSymbol(Suit suit) => suit switch
    {
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        _ => "♠"
    };

    public static string RankText(Rank rank) => rank switch
    {
        Rank.Ace => "A",
        Rank.Jack => "Kn",
        Rank.Queen => "D",
        Rank.King => "K",
        _ => ((int)rank).ToString()
    };
}