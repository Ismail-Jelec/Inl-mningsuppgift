using System.Text.Json;

public class GameStorage
{
    private readonly string _path;

    public GameStorage(string path)
    {
        _path = path;
    }

    public string? LastError { get; private set; }
    public List<PlayerRecord> Load()
    {
        LastError = null;
    

        if (!File.Exists(_path))
        {
            return new List<PlayerRecord>();
        }

        try
        {
            string json = File.ReadAllText(_path);   
            return JsonSerializer.Deserialize<List<PlayerRecord>>(json) ?? new List<PlayerRecord>();
        }
        catch (Exception ex) when (ex is IOException or JsonException or UnauthorizedAccessException)
        {
            LastError = $"kunde inte läsa {_path}: {ex.Message}";
            return new List<PlayerRecord>();
        } 
    }

    public bool Save(IEnumerable<PlayerRecord> records)
    {
        LastError = null;

        try
        {
            string json = JsonSerializer.Serialize(records);
            File.WriteAllText(_path, json);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            LastError = $"Kunde inte spara {_path}: {ex.Message}"; 
            return false;

        }
    }
}