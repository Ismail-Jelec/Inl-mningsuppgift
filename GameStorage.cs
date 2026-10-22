using System.Text.Json;

// Läser och skriver spelarnas resultat till en JSON-fil.
public class GameStorage
{
    private const string FilePath = "players.json";


    public string? LastError { get; private set; }
    public List<PlayerRecord> Load()
    {
        LastError = null;
    

        if (!File.Exists(FilePath))
        {
            return new List<PlayerRecord>();
        }

        try
        {
            string json = File.ReadAllText(FilePath);   
            return JsonSerializer.Deserialize<List<PlayerRecord>>(json) ?? new List<PlayerRecord>();
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            LastError = $"Kunde inte läsa {FilePath}: {ex.Message}";
            return new List<PlayerRecord>();
        } 
    }

    public bool Save(IEnumerable<PlayerRecord> records)
    {
        LastError = null;

        try
        {
            string json = JsonSerializer.Serialize(records);
            File.WriteAllText(FilePath, json);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            LastError = $"Kunde inte spara {FilePath}: {ex.Message}"; 
            return false;

        }
    }
}