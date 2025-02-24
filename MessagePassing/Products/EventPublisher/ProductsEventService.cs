using MessagePassing.Products.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MessagePassing.Products.EventPublisher;

public interface IProductsEventService
{
    Task ProcessEventsAsync(CancellationToken stoppingToken);
}

public class ProductsEventService(
    ILogger<ProductsEventService> logger,
    IRabbitMQPublisher rabbitMQPublisher,
    AppDbContext dbContext) : IProductsEventService
{
    private readonly ILogger<ProductsEventService> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRabbitMQPublisher _rabbitMQPublisher = rabbitMQPublisher
        ?? throw new ArgumentNullException(nameof(rabbitMQPublisher));
    private readonly AppDbContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task ProcessEventsAsync(CancellationToken stoppingToken)
    {
        //var factory = new AppDbContextFactory();
        //using var _dbContext = factory.CreateDbContext(Array.Empty<string>());
        var outboxMessages = await _dbContext.ProductAddedOutboxes.Where(x => !x.IsProcessed).Take(10).ToListAsync(stoppingToken);
        _logger.LogInformation("Processing {Count} outbox messages", outboxMessages.Count);

        foreach (var outboxMessage in outboxMessages)
        {
            await _rabbitMQPublisher.PublishMessageAsync(
                "product.exchange",
                "product.queue",
                "product",
                JsonSerializer.Serialize(outboxMessage));
            _logger.LogInformation("Product {Id} published: ", outboxMessage.Id);
            outboxMessage.IsProcessed = true;
        }

        await _dbContext.SaveChangesAsync(stoppingToken);
    }
}