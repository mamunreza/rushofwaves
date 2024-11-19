namespace SimpleDelegates.Maths;

public class MathWork
{
    public delegate int MathDelegate(int x, int y);

    public int Add(int x, int y)
    {
        return x + y;
    }

    public int Subtract(int x, int y)
    {
        return x - y;
    }
}
