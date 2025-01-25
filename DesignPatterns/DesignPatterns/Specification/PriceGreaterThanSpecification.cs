namespace DesignPatterns.Specification;

// Concrete specification for checking if a product price is greater than a threshold
public class PriceGreaterThanSpecification<T> : ISpecification<T> where T : IProduct
{
    private readonly decimal _threshold;

    public PriceGreaterThanSpecification(decimal threshold)
    {
        _threshold = threshold;
    }

    public bool IsSatisfiedBy(T candidate)
    {
        return candidate.Price > _threshold;
    }
}