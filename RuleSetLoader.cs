public class RuleSetLoader
{
    private const string FilePath = "rules.txt";

    // Program.cs skriver ut varningarna. Den här klassen skriver aldrig till konsolen.
    public List<string> Warnings { get; } = new();

    public RuleSet Load()
    {
        var rules = new RuleSet();

        if (!File.Exists(FilePath))
        {
            Warnings.Add($"Hittade inte {FilePath}. Standardregler används.");
            return rules;
        }

        string[] lines;
        try
        {
            lines = File.ReadAllLines(FilePath);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Warnings.Add($"Kunde inte läsa {FilePath}: {ex.Message}. Standardregler används.");
            return rules;
        }

        foreach (string line in lines)
        {
            string trimmed = line.Trim();

            // Tomma rader och kommentarer (#) hoppas över.
            if (trimmed.Length == 0 || trimmed.StartsWith('#'))
            {
                continue;
            }

            string[] parts = trimmed.Split('=');
            if (parts.Length != 2)
            {
                Warnings.Add($"Förstår inte raden \"{trimmed}\" i {FilePath}.");
                continue;
            }

            string key = parts[0].Trim();
            string value = parts[1].Trim();
            bool ok;

            switch (key)
            {
                case "DealerHitsSoft17":
                    ok = bool.TryParse(value, out bool soft);
                    if (ok) rules.DealerHitsSoft17 = soft;
                    break;
                case "DeckCount":
                    ok = int.TryParse(value, out int decks) && decks >= 1 && decks <= 8;
                    if (ok) rules.DeckCount = decks;
                    break;
                case "ReshuffleThreshold":
                    ok = int.TryParse(value, out int threshold) && threshold >= 15;
                    if (ok) rules.ReshuffleThreshold = threshold;
                    break;
                case "NaturalBeatsThreeCard21":
                    ok = bool.TryParse(value, out bool natural);
                    if (ok) rules.NaturalBeatsThreeCard21 = natural;
                    break;
                default:
                    ok = false;
                    break;
            }

            if (!ok)
            {
                Warnings.Add($"Ogiltig rad \"{trimmed}\" i {FilePath}. Standardvärdet används.");
            }
        }

        return rules;
    }
}