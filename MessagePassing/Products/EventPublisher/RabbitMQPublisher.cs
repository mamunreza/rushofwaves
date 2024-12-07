using RabbitMQ.Client;
using System.Text;

public interface IRabbitMQPublisher
{
    Task PublishMessageAsync(string exchangeName, string queueName, string routingKey, string message);
}

public class RabbitMQPublisher(
    ILogger<RabbitMQPublisher> logger,
    IMessagePublishRetryPolicy messagePublishRetryPolicy) : IRabbitMQPublisher
{
    private readonly ILogger<RabbitMQPublisher> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMessagePublishRetryPolicy _messagePublishRetryPolicy = messagePublishRetryPolicy
        ?? throw new ArgumentNullException(nameof(messagePublishRetryPolicy));

    public async Task PublishMessageAsync(string exchangeName, string queueName, string routingKey, string message)
    {
        var retryPolicy = _messagePublishRetryPolicy.ApplyAsync();

        await retryPolicy.ExecuteAsync(async () =>
        {
            var factory = new ConnectionFactory()
            {
                // HostName = "rabbitmq"
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                Port = int.Parse(Environment.GetEnvironmentVariable("RABBITMQ_PORT")),
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync(exchange: exchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false);
            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(queue: queueName, exchange: exchangeName, routingKey: routingKey);

            var body = Encoding.UTF8.GetBytes(message);
            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: routingKey,
                basicProperties: new BasicProperties { Persistent = true },
                body: body,
                mandatory: true
            );

            _logger.LogInformation("Message sent: {Message}", message);
        });
    }
}