//Bestämmer om spelaren ska stanna eller köpa 
// Player frågar det här istället för att fråga konsolen själv.
// Då kan man lägga till datorspelare senare utan att ändra i Player
public interface IPlayerStrategy
{
    Move Decide(Hand hand, Card dealerUpcard);
}