namespace DesignPatterns.Specification;

// Sample product class
public class Product : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool InStock { get; set; }
}