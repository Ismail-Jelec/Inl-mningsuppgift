
// Strategin för en mänsklig spelare. Den visar handen och frågar
// användaren om köpa eller stanna via ConsoleUi.
// Det är bara här och i ConsoleUi som det finns kontakt med användaren.
public class HumanStrategy : IPlayerStrategy
{
    private readonly ConsoleUi _ui;

    public HumanStrategy(ConsoleUi ui)
    {
        _ui = ui;
    }

    public Move Decide(Hand hand, Card dealerUpcard)
    {
        _ui.ShowHand(hand, dealerUpcard);
        return _ui.AskMove();
    }
}