// Klassen för en spelare. Den ärver från Participant så den har namn och hand.
// Player bestämmer inte själv om den ska köpa eller stanna, det gör strategin
// Vi gjorde så för att kunna ha datorspelare också och för att inte ha Console här.
public class Player : Participant

{
    private readonly IPlayerStrategy _strategy;

    // Resultaten sparas till fil av GameStorage och läses in igen vid nästa start.
    public int Wins { get; private set; }
    public int Losses { get; private set; }
    public int Pushes { get; private set; }

    public Player(string name, IPlayerStrategy strategy, int wins, int losses, int pushes) : base(name)
    {
        _strategy = strategy;
        Wins = wins;
        Losses = losses;
        Pushes = pushes;
    }
public Move Decide (Card DealerUpcard) => _strategy.Decide(Hand, DealerUpcard);

    // Räknar upp rätt siffra efter en runda.
    public void RecordOutcome(Outcome outcome)
    {
        switch (outcome)
        {
            case Outcome.Win: Wins++; break;
            case Outcome.Loss: Losses++; break;
            case Outcome.Draw: Pushes++; break;
        }
    }
}