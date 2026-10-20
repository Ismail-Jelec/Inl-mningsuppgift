// Håller en spelares marker och insatsen för den pågående rundan.
// Skriver aldrig ut något, det gör ConsoleUi.
public class Bankroll
{
    public const int StartingChips = 100;

    public int Chips { get; private set; }
    public int CurrentBet { get; private set; }

    // Sant när spelaren inte har några marker kvar och måste lämna bordet.
    public bool IsEmpty => Chips == 0;

    public Bankroll(int chips)
    {
        Chips = chips;
    }

    // Insatsen får inte vara noll och inte större än markerna.
    public bool IsValidBet(int amount) => amount > 0 && amount <= Chips;

    // Insatsen dras direkt, den ligger på bordet under rundan.
    public void PlaceBet(int amount)
    {
        if (!IsValidBet(amount))
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Ogiltig insats.");
        }
        Chips -= amount;
        CurrentBet = amount;
    }

    // Betalar ut efter rundan och nollställer insatsen.
    public void Settle(Outcome outcome)
    {
        Chips += Payout(outcome);
        CurrentBet = 0;
    }

    // Det som läggs tillbaka till markerna, insatsen inräknad.
    // Naturlig tjugoett 3:2, vinst 1:1, lika ger insatsen tillbaka, förlust ger inget.
    private int Payout(Outcome outcome) => outcome switch
    {
        Outcome.Blackjack => CurrentBet + CurrentBet * 3 / 2,
        Outcome.Win => CurrentBet * 2,
        Outcome.Draw => CurrentBet,
        _ => 0
    };
}