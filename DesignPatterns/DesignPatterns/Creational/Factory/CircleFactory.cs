namespace DesignPatterns.Creational.Factory;

// Concrete factory classes
public class CircleFactory : IShapeFactory
{
    public IShape CreateShape()
    {
        return new Circle();
    }
}
