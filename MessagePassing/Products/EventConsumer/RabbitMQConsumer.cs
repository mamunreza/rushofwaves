namespace EventConsumer;

public interface IRabbitMQConsumer
{
    Task ConsumeAsync(CancellationToken cancellation);
}

public class RabbitMQConsumer : IRabbitMQConsumer
{
    private readonly ILogger<RabbitMQConsumer> _logger;

    public RabbitMQConsumer(ILogger<RabbitMQConsumer> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ConsumeAsync(CancellationToken cancellation)
    {
        await Task.CompletedTask;
        _logger.LogInformation("Consuming messages from RabbitMQ");
    }
}
