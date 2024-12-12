using EventPublisher.Infrastructure;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

public interface IRabbitMQPublisher
{
    Task PublishMessageAsync(string exchangeName, string queueName, string routingKey, string message);
}

public class RabbitMQPublisher : IRabbitMQPublisher
{
    private readonly ILogger<RabbitMQPublisher> _logger;
    private readonly IMessagePublishRetryPolicy _messagePublishRetryPolicy;
    private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;

    public RabbitMQPublisher(
        ILogger<RabbitMQPublisher> logger,
        IMessagePublishRetryPolicy messagePublishRetryPolicy,
        IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _messagePublishRetryPolicy = messagePublishRetryPolicy ?? throw new ArgumentNullException(nameof(messagePublishRetryPolicy));
        _rabbitMqConfiguration = rabbitMqConfiguration ?? throw new ArgumentNullException(nameof(rabbitMqConfiguration));

        ValidateConfiguration(_rabbitMqConfiguration.Value);
    }

    public async Task PublishMessageAsync(string exchangeName, string queueName, string routingKey, string message)
    {
        var retryPolicy = _messagePublishRetryPolicy.ApplyAsync();

        await retryPolicy.ExecuteAsync(async () =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ__HOST") ?? _rabbitMqConfiguration.Value.Host,
                Port = int.TryParse(Environment.GetEnvironmentVariable("RABBITMQ__PORT"), out var port) ? port : _rabbitMqConfiguration.Value.Port,
                UserName = Environment.GetEnvironmentVariable("RABBITMQ__USERNAME") ?? _rabbitMqConfiguration.Value.Username,
                Password = Environment.GetEnvironmentVariable("RABBITMQ__PASSWORD") ?? _rabbitMqConfiguration.Value.Password
            };

            try
            {
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
            }
            catch (BrokerUnreachableException ex)
            {
                _logger.LogError(ex, "Could not reach the broker. Message: {Message}", message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while publishing the message. Message: {Message}", message);
                throw;
            }
        });
    }

    private void ValidateConfiguration(RabbitMqConfiguration config)
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RABBITMQ__HOST")) &&
            string.IsNullOrWhiteSpace(config.Host))
            throw new ArgumentException("RabbitMQ host is not configured.", nameof(config.Host));
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RABBITMQ__PORT")) &&
            config.Port <= 0)
            throw new ArgumentException("RabbitMQ port is not configured or invalid.", nameof(config.Port));
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RABBITMQ__USERNAME")) &&
            string.IsNullOrWhiteSpace(config.Username))
            throw new ArgumentException("RabbitMQ username is not configured.", nameof(config.Username));
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RABBITMQ__PASSWORD")) &&
            string.IsNullOrWhiteSpace(config.Password))
            throw new ArgumentException("RabbitMQ password is not configured.", nameof(config.Password));
    }
}
