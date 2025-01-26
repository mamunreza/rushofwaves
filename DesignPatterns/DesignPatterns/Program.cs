using DesignPatterns.Specification;
using DesignPatterns.Strategy;

namespace DesignPatterns;

class Program
{
    static void Main(string[] args)
    {
        //ImplementSpecificationPattern();
        ImplementStrategyPattern();
    }

    private static void ImplementStrategyPattern()
    {
        var survey1 = new CustomerSurvey
        {
            CustomerAge = 25,
            CustomerLocation = "New York"
        };

        ICustomerSurveyFilter ageFilter = new AgeBasedFilter(18);
        ICustomerSurveyFilter locationFilter = new LocationBasedFilter("New York");

        Console.WriteLine($"Age Filter Satisfied: {ageFilter.IsSatisfied(survey1)}");
        Console.WriteLine($"Location Filter Satisfied: {locationFilter.IsSatisfied(survey1)}");
    }

    private static void ImplementSpecificationPattern()
    {
        // Create some sample products
        var product1 = new Product { Name = "Product A", Price = 10, InStock = true };
        var product2 = new Product { Name = "Product B", Price = 20, InStock = false };
        var product3 = new Product { Name = "Product C", Price = 15, InStock = true };

        // Create specifications
        var inStockSpec = new InStockSpecification<Product>();
        var priceGreaterThan15Spec = new PriceGreaterThanSpecification<Product>(15);

        // Create composite specifications
        var inStockAndPriceGreaterThan15Spec = new AndSpecification<Product>(inStockSpec, priceGreaterThan15Spec);
        var inStockOrPriceGreaterThan15Spec = new OrSpecification<Product>(inStockSpec, priceGreaterThan15Spec);

        // Test the specifications
        Console.WriteLine($"Product 1 meets inStockSpec: {inStockSpec.IsSatisfiedBy(product1)}");
        Console.WriteLine($"Product 1 meets priceGreaterThan15Spec: {priceGreaterThan15Spec.IsSatisfiedBy(product1)}");
        Console.WriteLine($"Product 1 meets inStockAndPriceGreaterThan15Spec: {inStockAndPriceGreaterThan15Spec.IsSatisfiedBy(product1)}");
        Console.WriteLine($"Product 1 meets inStockOrPriceGreaterThan15Spec: {inStockOrPriceGreaterThan15Spec.IsSatisfiedBy(product1)}");

        Console.ReadLine();
    }
}