using SimpleDelegates.Basics;
using SimpleDelegates.Maths;
using static SimpleDelegates.Basics.ConsolePrint;
using static SimpleDelegates.Basics.DelegateContainer;
using static SimpleDelegates.Maths.MathWork;

namespace SimpleDelegates;

internal static class SimpleHelpers
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

    internal static void ExecuteSimpleLogDelegateWork()
    {
        #region Simple Delegate
        //LogDelegate logDelegate = new(SimpleLog.LogTextToScreen);
        //LogDelegate logDelegate = new(SimpleLog.LogTextToFile);
        //LogDelegate logDelegate = new(new SimpleLog2().LogTextToScreen);
        //LogDelegate logDelegate = new(new SimpleLogInstanceMethod().LogTextToFile);
        //logDelegate("Simple log message."); 
        #endregion

        #region Multi cast delegate
        //var consoleLogger = new SimpleLogInstanceMethod().LogTextToScreen;
        //var fileLogger = new SimpleLogInstanceMethod().LogTextToFile;

        //var consoleLoggerDelegate = new LogDelegate(consoleLogger);
        //var fileLoggerDelegate = new LogDelegate(fileLogger);

        //LogDelegate multiCastLogDelegate = consoleLoggerDelegate + fileLoggerDelegate;
        //multiCastLogDelegate("Multi cast delegate log message."); 
        #endregion

        #region Passing delegate as parameter
        var consoleLogger = new SimpleLogInstanceMethod().LogTextToScreen;
        var fileLogger = new SimpleLogInstanceMethod().LogTextToFile;
        var paramDelegateLogger = new SimpleLogInstanceMethod().LogTextToScreenFromDelegateParameter;

        var consoleLoggerDelegate = new LogDelegate(consoleLogger);
        var fileLoggerDelegate = new LogDelegate(fileLogger);

        LogDelegate multiCastLogDelegate = consoleLoggerDelegate + fileLoggerDelegate;
        paramDelegateLogger(multiCastLogDelegate, "Passing delegate as parameter log message.");
        #endregion
    }
}