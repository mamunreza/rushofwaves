namespace DesignPatterns.Behavioral.Strategy;

// 1. Strategy Interface
public interface IShippingStrategy
{
    double CalculateCost(Order order);
}
