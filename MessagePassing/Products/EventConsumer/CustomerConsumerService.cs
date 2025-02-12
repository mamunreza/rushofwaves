using EventConsumer;

namespace MessagePassing.Products.EventConsumer;

public interface ICustomerConsumerService
{
    Task ConsumeAsync(CancellationToken cancellation);
}

public class CustomerConsumerService : ICustomerConsumerService
{
    private readonly ILogger<CustomerConsumerService> _logger;
    private readonly IRabbitMQConsumer _rabbitMQConsumer;

    public CustomerConsumerService(
        ILogger<CustomerConsumerService> logger,
        IRabbitMQConsumer rabbitMQConsumer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _rabbitMQConsumer = rabbitMQConsumer 
            ?? throw new ArgumentNullException(nameof(rabbitMQConsumer));
    }

    public async Task ConsumeAsync(CancellationToken cancellation)
    {
        await _rabbitMQConsumer.ConsumeAsync(cancellation);
        _logger.LogInformation("Consumption started");
    }
}
