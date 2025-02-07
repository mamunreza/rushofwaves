namespace DesignPatterns.Creational.Factory;

// Concrete product classes
public class Circle : IShape
{
    public void Draw()
    {
        Console.WriteLine("Drawing a Circle");
    }
}
