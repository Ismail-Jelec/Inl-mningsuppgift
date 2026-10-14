// Det Player och Dealer har gemensamt: ett namn, en hand och att de kan ta
// emot kort. Hur de bestämmer sig ligger i subklasserna.
public abstract class Participant
{
    public string Name { get; }
    public Hand Hand { get; } = new();

    protected Participant(string name)
    {
        Name = name;
    }

    public void ReceiveCard(Card card) => Hand.Add(card);

    public virtual void ClearHand() => Hand.Clear();
}
