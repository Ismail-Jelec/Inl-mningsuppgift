// Strategin för en mänsklig spelare. Den visar handen och frågar
// användaren om köpa eller stanna via ConsoleUi.
// Det är bara här och i ConsoleUi som det finns kontakt med användaren.
public class HumanStrategy : IPlayerStrategy
{
    private readonly ConsoleUi _ui;
    private readonly string _name;

    public HumanStrategy(ConsoleUi ui, string name)
    {
        _ui = ui;
        _name = name;
    }

    public Move Decide(Hand hand, Card dealerUpcard)
    {
        return _ui.AskMove(_name, hand, dealerUpcard);
    }

    public int ChooseBet(int chips)
    {
        return _ui.AskBet(_name, chips);
    }
}