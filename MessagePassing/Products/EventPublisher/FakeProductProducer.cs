using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePassing.Products.Domain;

public class FakeProductProducer
{
    private static readonly Random _random = new Random();

    public static ProductAdded ProduceRandomProduct()
    {
        var productNames = new List<string> { "Laptop", "Smartphone", "Tablet", "Monitor", "Keyboard" };
        var productCategories = new List<string> { "Electronics", "Accessories", "Home Appliances" };

        var product = new ProductAdded
        {
            Id = Guid.NewGuid(),
            Name = productNames[_random.Next(productNames.Count)],
            Category = productCategories[_random.Next(productCategories.Count)],
            Price = _random.Next(100, 2000) + _random.Next(),
            Stock = _random.Next(1, 100)
        };

        return product;
    }
}