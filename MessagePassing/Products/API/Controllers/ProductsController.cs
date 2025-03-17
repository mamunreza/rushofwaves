using MessagePassing.Products.API.Services;
using MessagePassing.Domain;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateProductAsync(ProductAdded product)
    {
        var created = await _productService.CreateAsync(product);
        await _productService.CreateProductAddedOutbox(product);
        _logger.LogInformation("Product added: {ProductId}", created.Id);
        return Ok(created);
    }

    //[HttpGet(Name = "GetProducts")]
    //public async Task<IActionResult> GetProducts()
    //{
    //    var products = await _dbContext.Products.ToListAsync();
    //    return Ok(products);
    //}

    
}
