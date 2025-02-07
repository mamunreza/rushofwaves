namespace DesignPatterns.Behavioral.Strategy;

public class OvernightShipping : IShippingStrategy
{
    public double CalculateCost(Order order)
    {
        return 20.0 + order.Weight * 0.5; // Example calculation
    }
}
