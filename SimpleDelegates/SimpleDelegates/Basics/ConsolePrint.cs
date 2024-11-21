namespace SimpleDelegates.Basics;

internal class ConsolePrint
{
    internal delegate void ConsolePrintDelegate();

    internal static void Method1()
    {
        Console.WriteLine("Method 1 invoked.");
    }

    internal static void Method2()
    {
        Console.WriteLine("Method 2 invoked.");
    }
}
