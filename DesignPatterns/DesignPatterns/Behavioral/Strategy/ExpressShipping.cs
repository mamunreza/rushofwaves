namespace DesignPatterns.Behavioral.Strategy;

public class ExpressShipping : IShippingStrategy
{
    public double CalculateCost(Order order)
    {
        return 10.0 + order.Weight * 0.2; // Example calculation
    }
}
