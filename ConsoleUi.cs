using System.Text;

public class ConsoleUi
{
    private const int MaxNameLength = 20;
    public ConsoleUi()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void ShowMessage(string text) => Console.WriteLine(text);

    public int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Skriv ett tal mellan {min} och {max}.");
        }
    }
    private static string ReadLine()
    {
        string? line = Console.ReadLine();
        if (line == null)
        {
            Environment.Exit(0);
        }
        return line.Trim();
    }

    public string AskName()
    {
        while (true)
        {
            Console.Write("Namn: ");
            string name = ReadLine();
            if (name.Length > 0 && name.Length <= MaxNameLength)
            {
                return name;
            }
            Console.WriteLine($"Namnet måste vara 1–{MaxNameLength} tecken.");
        }
    }

    // Visar spelarens hand och givarens öppna kort, aldrig hålkortet.
    // Frågar tills svaret är k eller s.
    public Move AskMove(string name, Hand hand, Card dealerUpCard)
    {
        Console.WriteLine();
        Console.WriteLine($"{name}: {FormatHand(hand)}");
        Console.WriteLine($"Givaren visar: {FormatCard(dealerUpCard)} [dolt]");

        while (true)
        {
            Console.Write("(k)öpa eller (s)tanna? ");
            string answer = ReadLine().ToLower();
            if (answer == "k") return Move.Hit;
            if (answer == "s") return Move.Stand;
            Console.WriteLine("Skriv k för att köpa eller s för att stanna.");
        }
    }

    public void ShowRoundResult(Dealer dealer, Dictionary<Player, Outcome> results)
    {
        Console.WriteLine();
        Console.WriteLine("--- Resultat ---");
        Console.WriteLine($"Givaren: {FormatHand(dealer.Hand)}");
        foreach (var (player, outcome) in results)
        {
            Console.WriteLine($"{player.Name}: {FormatHand(player.Hand)} – {FormatOutcome(outcome)}");
        }
    }

    // Till exempel "♥A ♠6 (mjuk 17)".
    private static string FormatHand(Hand hand)
    {
        string cards = string.Join(" ", hand.Cards.Select(FormatCard));
        string value = hand.IsBust ? $"{hand.Value}, spräckt"
            : hand.IsNatural ? "naturlig tjugoett"
            : hand.IsSoft ? $"mjuk {hand.Value}"
            : hand.Value.ToString();
        return $"{cards} ({value})";
    }

    private static string FormatCard(Card card) => SuitSymbol(card.Suit) + RankText(card.Rank);

    private static string SuitSymbol(Suit suit) => suit switch
    {
        Suit.Hearts => "♥",
        Suit.Diamonds => "♦",
        Suit.Clubs => "♣",
        _ => "♠"
    };

    private static string RankText(Rank rank) => rank switch
    {
        Rank.Ace => "A",
        Rank.Jack => "Kn",
        Rank.Queen => "D",
        Rank.King => "K",
        _ => ((int)rank).ToString()
    };

    private static string FormatOutcome(Outcome outcome) => outcome switch
    {
        Outcome.Win => "Vinst",
        Outcome.Loss => "Förlust",
        _ => "Lika"
    };
}