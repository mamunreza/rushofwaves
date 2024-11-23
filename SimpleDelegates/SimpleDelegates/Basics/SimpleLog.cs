namespace SimpleDelegates.Basics;

internal class SimpleLog
{
    internal static void LogTextToScreen(string message)
    {
        Console.WriteLine($"{DateTime.UtcNow} : {message}");
    }

    internal static void LogTextToFile(string message)
    {
        File.AppendAllText("log.txt", $"{DateTime.UtcNow} : {message}");
    }
}
