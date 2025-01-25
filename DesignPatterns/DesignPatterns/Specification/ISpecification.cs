namespace DesignPatterns.Specification;

// Define the base interface for specifications
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T candidate);
}