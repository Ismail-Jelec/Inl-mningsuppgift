// Husreglerna. Läses in från rules.txt när programmet startar.
public class RuleSet
{
    // Sant: givaren köper på mjuk 17. Falskt: givaren stannar.
    public bool DealerHitsSoft17 { get; set; } = false;

    // Antal lekar à 52 kort.
    public int DeckCount { get; set; } = 1;

    // Leken blandas om när det är färre kort än så kvar.
    public int ReshuffleThreshold { get; set; } = 15;

    // Sant: naturlig tjugoett slår 21 på tre kort eller fler.
    // Falskt: de är lika värda och rundan blir lika.
    public bool NaturalBeatsThreeCard21 { get; set; } = true;
}