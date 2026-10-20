// Det som sparas om en spelare mellan körningar.
public record PlayerRecord(string Name, int Wins, int Losses, int Pushes, int Chips = Bankroll.StartingChips);
