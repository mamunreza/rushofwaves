using MessagePassing.Domain;
using MessagePassing.Products.Data;

namespace MessagePassing.Products.API.Services;

public interface IProductService
{
    Task<Product> CreateAsync(ProductAdded productAdded);
    Task<ProductAddedOutbox> CreateProductAddedOutbox(ProductAdded product);
}

public class ProductService(
    ILogger<ProductService> logger,
    AppDbContext dbContext) : IProductService
{
    private readonly ILogger<ProductService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<Product> CreateAsync(ProductAdded productAdded)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = productAdded.Name,
            Price = productAdded.Price,
            Description = productAdded.Description,
            Category = productAdded.Category,
            ImageUrl = productAdded.ImageUrl,
            Brand = productAdded.Brand,
            Stock = 1,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _dbContext.Add(product);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Product added: {ProductId}", product.Id);
        return product;
    }

    public async Task<ProductAddedOutbox> CreateProductAddedOutbox(ProductAdded product)
    {
        var outboxItem = new ProductAddedOutbox
        {
            Id = Guid.NewGuid(),
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Category = product.Category,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand,
            Stock = 1,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _dbContext.Add(outboxItem);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Product added to outbox: {ProductId}", outboxItem.Id);
        return outboxItem;
    }
}
