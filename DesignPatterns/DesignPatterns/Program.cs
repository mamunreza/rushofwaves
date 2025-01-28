using DesignPatterns.PredicateBasedFilter;
using DesignPatterns.QueryObject;
using DesignPatterns.Specification;
using DesignPatterns.Strategy;

namespace DesignPatterns;

partial class Program
{
    static void Main(string[] args)
    {
        //ImplementSpecificationPattern();
        //ImplementStrategyPattern();
        //ImplementPredicateBasedFiltering();
        ImplementQueryObjectPattern();
    }

    private static void ImplementQueryObjectPattern()
    {
        var surveys = new List<QueryObject.CustomerSurvey>()
        {
            new QueryObject.CustomerSurvey { CustomerAge = 25, CustomerLocation = "New York" },
            new QueryObject.CustomerSurvey { CustomerAge = 17, CustomerLocation = "New York" },
            new QueryObject.CustomerSurvey { CustomerAge = 30, CustomerLocation = "California" },
            new QueryObject.CustomerSurvey { CustomerAge = 20, CustomerLocation = "New York" },
            new QueryObject.CustomerSurvey { CustomerAge = 15, CustomerLocation = "California" }
        };

        // Create queries
        var ageQuery = new AgeBasedQuery(18);
        var locationQuery = new LocationBasedQuery("New York");
        var combinedQuery = new CombinedQuery(ageQuery, locationQuery);

        // Execute queries
        var filteredSurveys1 = ageQuery.Execute(surveys);
        var filteredSurveys2 = locationQuery.Execute(surveys);
        var combinedFilteredSurveys = combinedQuery.Execute(surveys);

        // Print results
        Console.WriteLine("Surveys from customers over 18:");
        foreach (var survey in filteredSurveys1)
        {
            Console.WriteLine($"Age: {survey.CustomerAge}, Location: {survey.CustomerLocation}");
        }

        Console.WriteLine("\nSurveys from customers in New York:");
        foreach (var survey in filteredSurveys2)
        {
            Console.WriteLine($"Age: {survey.CustomerAge}, Location: {survey.CustomerLocation}");
        }

        Console.WriteLine(
            "\nSurveys from customers over 18 and in New York:");
        foreach (var survey in combinedFilteredSurveys)
        {
            Console.WriteLine($"Age: {survey.CustomerAge}, Location: {survey.CustomerLocation}");
        }
    }

    private static void ImplementPredicateBasedFiltering()
    {
        List<PredicateBasedFilter.CustomerSurvey> surveys =
        [
            new PredicateBasedFilter.CustomerSurvey { CustomerAge = 25, CustomerLocation = "New York" },
            new PredicateBasedFilter.CustomerSurvey { CustomerAge = 17, CustomerLocation = "New York" },
            new PredicateBasedFilter.CustomerSurvey { CustomerAge = 30, CustomerLocation = "California" },
            new PredicateBasedFilter.CustomerSurvey { CustomerAge = 20, CustomerLocation = "New York" },
            new PredicateBasedFilter.CustomerSurvey { CustomerAge = 15, CustomerLocation = "California" }
        ];

        // Filter surveys from New York
        var newYorkSurveys = surveys.Where(SurveyFilters.IsFromNewYork);
        Console.WriteLine("Surveys from New York:");
        foreach (var survey in newYorkSurveys)
        {
            Console.WriteLine(survey);
        }

        // Filter surveys where customers are over 18
        var adultSurveys = surveys.Where(SurveyFilters.IsOver18);
        Console.WriteLine("\nSurveys from adults:");
        foreach (var survey in adultSurveys)
        {
            Console.WriteLine(survey);
        }
    }

    private static void ImplementStrategyPattern()
    {
        var survey1 = new Strategy.CustomerSurvey
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
