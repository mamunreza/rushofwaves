namespace DesignPatterns.Specification;

// Concrete specification for checking if a product is in stock
public class InStockSpecification<T> : ISpecification<T> where T : IProduct
{
    public bool IsSatisfiedBy(T candidate)
    {
        return candidate.InStock;
    }
}