// Givaren gör inga val utan följer en fast regel. Regeln ligger här och inte
// i Round, så att Round inte behöver känna till hur varje deltagare spelar.
public class Dealer : Participant
{
    public const int StandValue = 17;

    public Dealer() : base("Givaren")
    {
    }

    // Hålkortet är givarens andra kort. Det är dolt tills givarens tur börjar.
    public bool HoleCardHidden { get; private set; } = true;

    public Card UpCard => Hand.Cards[0];

    public void RevealHoleCard()
    {
        HoleCardHidden = false;
    }

    // Köper på 16 eller lägre, stannar på 17 eller högre, även på mjuk 17.
    public bool ShouldHit()
    {
        return Hand.Value < StandValue;
    }

    public override void ClearHand()
    {
        base.ClearHand();
        HoleCardHidden = true;
    }
}
