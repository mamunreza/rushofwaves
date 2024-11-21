using SimpleDelegates.Basics;
using SimpleDelegates.Maths;
using static SimpleDelegates.Basics.ConsolePrint;
using static SimpleDelegates.Maths.MathWork;

namespace SimpleDelegates;

internal static class ProgramHelpers
{

    internal static void ExecuteSimpleDelegateWork()
    {
        MathWork mathWork = new();
        MathDelegate addDelegate = new(mathWork.Add);
        MathDelegate subtractDelegate = new(mathWork.Subtract);

        int result = addDelegate(10, 20);
        Console.WriteLine($"Addition: {result}");
        int result2 = subtractDelegate(20, 10);
        Console.WriteLine($"Subtraction: {result2}");
    }

    internal static void ExecuteMultiCastDelegateWork()
    {
        ConsolePrintDelegate del1 = new(ConsolePrint.Method1);
        ConsolePrintDelegate del2 = new(ConsolePrint.Method2);
        ConsolePrintDelegate multiCastDelegate = del1 + del2;
        multiCastDelegate();
    }
}