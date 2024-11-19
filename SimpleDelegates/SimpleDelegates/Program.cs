using SimpleDelegates.Maths;
using static SimpleDelegates.Maths.MathWork;

namespace SimpleDelegates;

internal class Program
{
    static void Main(string[] args)
    {
        //Console.WriteLine("Hello, World!");

        MathWork mathWork = new();
        MathDelegate addDelegate = new(mathWork.Add);
        MathDelegate subtractDelegate = new(mathWork.Subtract);

        int result = addDelegate(10, 20);
        Console.WriteLine($"Addition: {result}");
        int result2 = subtractDelegate(20, 10);
        Console.WriteLine($"Subtraction: {result2}");
    }
}
