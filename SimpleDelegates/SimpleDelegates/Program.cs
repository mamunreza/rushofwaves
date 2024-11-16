namespace SimpleDelegates;

internal class Program
{
    public delegate void ShowMessageDelegate(string message);
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");

        ShowMessageDelegate simpleDelegate = new ShowMessageDelegate(ShowMessage);
        simpleDelegate("Hello, World!");

    }

    public static void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
