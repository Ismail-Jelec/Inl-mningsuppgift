public interface IPlayerStrategy
{
    Move Decide(Hand hand, Card dealerUpcard);
}