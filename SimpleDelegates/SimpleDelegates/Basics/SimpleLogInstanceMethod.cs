using static SimpleDelegates.Basics.DelegateContainer;

namespace SimpleDelegates.Basics;

internal class SimpleLogInstanceMethod
{
    internal void LogTextToScreen(string message)
    {
        Console.WriteLine($"{DateTime.UtcNow} : {message}");
    }
    internal void LogTextToFile(string message)
    {
        File.AppendAllText("log.txt", $"{DateTime.UtcNow} : {message}\n");
    }

    internal void LogTextToScreenFromDelegateParameter(LogDelegate logDelegate, string message)
    {
        logDelegate(message);
    }
}
