// Ett kort har bara färg och valör. Värdet räknas i Hand, eftersom essets
// värde beror på resten av handen.
public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }
}
