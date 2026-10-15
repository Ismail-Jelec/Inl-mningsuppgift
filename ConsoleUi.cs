using System.Text;

public class ConsoleUi
{
    private const int MaxNameLength = 20;
    public ConsoleUi()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void ShowMessage(string text) => Console.WriteLine(text);

    public int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }
            Console.WriteLine($"Skriv ett tal mellan {min} och {max}.");
        }
    }
    private static string ReadLine()
    {
        string? line = Console.ReadLine();
        if (line == null)
        {
            Environment.Exit(0);
        }
        return line.Trim();
    }


}