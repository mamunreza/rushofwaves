using MessagePassing.Common.RabbitMq;
using MessagePassing.Products.Data;

namespace MessagePassing.Products.EventConsumer;

public interface ICustomerConsumerService
{
    Task ConsumeAsync(CancellationToken cancellation);
}

public class CustomerConsumerService : ICustomerConsumerService
{
    private readonly ILogger<CustomerConsumerService> _logger;
    private readonly IRabbitMQConsumer _rabbitMQConsumer;
    private readonly AppDbContext _appDbContext;

    public CustomerConsumerService(
        ILogger<CustomerConsumerService> logger,
        IRabbitMQConsumer rabbitMQConsumer,
        AppDbContext appDbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _rabbitMQConsumer = rabbitMQConsumer 
            ?? throw new ArgumentNullException(nameof(rabbitMQConsumer));
        _appDbContext = appDbContext
            ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public async Task ConsumeAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Consumption started");
        await _rabbitMQConsumer.StartAsync<CustomerAddedInbox>(ProcessCustomerAddedAsync, cancellationToken);
        _logger.LogInformation("Consumption ended");
    }

    private async Task ProcessCustomerAddedAsync(CustomerAddedInbox message, CancellationToken cancellationToken)
    {
        _appDbContext.Add(message);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
