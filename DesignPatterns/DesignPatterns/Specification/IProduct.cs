namespace DesignPatterns.Specification;

// Interface for products
public interface IProduct
{
    string Name { get; set; }
    decimal Price { get; set; }
    bool InStock { get; set; }
}