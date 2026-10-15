// Klassen för en spelare. Den ärver från Participant så den har namn och hand.
// Player bestämmer inte själv om den ska köpa eller stanna, det gör strategin
// Vi gjorde så för att kunna ha datorspelare också och för att inte ha Console här.
public class Player : Participant

{
    private readonly IPlayerStrategy _strategy; 

    public Player(string name, IPlayerStrategy strategy) : base(name)
    {
        _strategy = strategy;
    }
public Move Decide (Card DealerUpcard) => _strategy.Decide(Hand, DealerUpcard);
}