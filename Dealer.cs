// Givaren gör inga val utan följer en fast regel. Regeln ligger här och inte
// i Round, så att Round inte behöver känna till hur varje deltagare spelar.
public class Dealer : Participant
{
    public const int StandValue = 17;

    private readonly RuleSet _rules;

    public Dealer(RuleSet rules) : base("Givaren")
    {
        _rules = rules;
    }

    // Hålkortet är givarens andra kort. Det är dolt tills givarens tur börjar.
    public bool HoleCardHidden { get; private set; } = true;

    public Card UpCard => Hand.Cards[0];

    public void RevealHoleCard()
    {
        HoleCardHidden = false;
    }

    // Köper på 16 eller lägre och stannar på 18 eller högre.
    // På 17 köps bara mjuk 17, och bara om husregeln säger det.
    public bool ShouldHit()
    {
        if (Hand.Value < StandValue)
        {
            return true;
        }

        return _rules.DealerHitsSoft17 && Hand.Value == StandValue && Hand.IsSoft;
    }
    public override void ClearHand()
    {
        base.ClearHand();
        HoleCardHidden = true;
    }
}
