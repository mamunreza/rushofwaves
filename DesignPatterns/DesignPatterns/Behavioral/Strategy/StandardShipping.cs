namespace DesignPatterns.Behavioral.Strategy;

// 2. Concrete Strategies
public class StandardShipping : IShippingStrategy
{
    public double CalculateCost(Order order)
    {
        return 5.0 + order.Weight * 0.1; // Example calculation
    }
}
