using MessagePassing.Products.API.Services;
using MessagePassing.Domain;
using Microsoft.AspNetCore.Mvc;
using API.Domain;

namespace MessagePassing.Products.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService _productService;

    public ProductsController(
        ILogger<ProductsController> logger,
        IProductService productService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }

    [HttpPost(Name = "createProductAsync")]
    public async Task<IActionResult> CreateProductAsync(ProdocutAddedCommand command)
    {
        var productAdded = new ProductAdded
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Price = command.Price,
            Description = command.Description,
            Category = command.ProductCategory.ToString(),
            ImageUrl = command.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        var created = await _productService.CreateAsync(productAdded);
        await _productService.CreateProductAddedOutbox(productAdded);
        _logger.LogInformation("Product added: {ProductId}", created.Id);
        return Ok(created);
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }


}
