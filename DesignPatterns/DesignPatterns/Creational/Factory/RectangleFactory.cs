namespace DesignPatterns.Creational.Factory;

public class RectangleFactory : IShapeFactory
{
    public IShape CreateShape()
    {
        return new Rectangle();
    }
}