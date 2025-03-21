namespace API.Domain;

public class ProdocutAddedCommand
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public string? ImageUrl { get; set; }
}

public enum ProductCategory
{
    Electronics,
    Clothing,
    Home,
    Garden,
    Beauty,
    Toys,
    Food,
    Other
}
