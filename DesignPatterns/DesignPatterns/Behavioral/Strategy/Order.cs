namespace DesignPatterns.Behavioral.Strategy;

// 3. Context
public class Order
{
    public double Weight { get; set; }
    public IShippingStrategy ShippingStrategy { get; set; }

    public Order(double weight, IShippingStrategy shippingStrategy)
    {
        Weight = weight;
        ShippingStrategy = shippingStrategy;
    }

    public double CalculateTotalShippingCost()
    {
        return ShippingStrategy.CalculateCost(this);
    }
}