using System.Text;

public class ConsoleUi
{
    private const int MaxNameLength = 20;
    public ConsoleUi()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    public void ShowMessage(string text) => Console.WriteLine(text);
}