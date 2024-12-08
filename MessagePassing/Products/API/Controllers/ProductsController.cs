using MessagePassing.Products.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly AppDbContext _dbContext;

    public ProductsController(
        ILogger<ProductsController> logger, 
        AppDbContext dbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    [HttpPost(Name = "AddProduct")]
    public async Task<IActionResult> AddProduct(Product product)
    {
        var newProduct = new Product
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
        _dbContext.Products.Add(newProduct);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Product added: {ProductId}", newProduct.Id);
        return Ok(newProduct);
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _dbContext.Products.ToListAsync();
        return Ok(products);
    }
}
