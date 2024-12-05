using System.Text.Json;

namespace EventPublisher;

public interface IProductsEventService
{
    Task PublishMessageAsync(string message);
}

public class ProductsEventService(
    ILogger<ProductsEventService> logger,
    IRabbitMQPublisher rabbitMQPublisher) : IProductsEventService
{
    private readonly ILogger<ProductsEventService> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRabbitMQPublisher _rabbitMQPublisher = rabbitMQPublisher
        ?? throw new ArgumentNullException(nameof(rabbitMQPublisher));

    public async Task PublishMessageAsync(string message)
    {
        await _rabbitMQPublisher.PublishMessageAsync(
            "product.exchange",
            "product.queue",
            "product",
            JsonSerializer.Serialize(message));
        _logger.LogInformation("Product {OrderId} published: ", 1);
    }
}