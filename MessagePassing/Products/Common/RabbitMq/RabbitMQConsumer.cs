using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MessagePassing.Common.RabbitMq;

public delegate Task MessageProcessedHandler<T>(T message, CancellationToken cancellationToken);

public interface IRabbitMQConsumer
{
    Task StartAsync<T>(MessageProcessedHandler<T> messageProcessedHandler, CancellationToken cancellationToken);
}


public class RabbitMQConsumer : IRabbitMQConsumer, IAsyncDisposable
{
    private readonly ILogger<RabbitMQConsumer> _logger;
    private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;

    private readonly string _exchangeName;
    private readonly string _queueName;
    private readonly string _routingKey;
    private IChannel? _channel;
    private IConnection? _connection;

    public RabbitMQConsumer(
        ILogger<RabbitMQConsumer> logger,
        IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        _logger = logger
            ?? throw new ArgumentNullException(nameof(logger));
        _rabbitMqConfiguration = rabbitMqConfiguration
            ?? throw new ArgumentNullException(nameof(rabbitMqConfiguration));

        _exchangeName = _rabbitMqConfiguration.Value.Customer.Exchange;
        _queueName = _rabbitMqConfiguration.Value.Customer.Queue;
        _routingKey = _rabbitMqConfiguration.Value.Customer.RoutingKey;
    }

    public async Task StartAsync<T>(MessageProcessedHandler<T> messageProcessedHandler, CancellationToken cancellationToken)
    {
        await CreateRabbitChannel(cancellationToken);

        if (_channel == null)
        {
            throw new InvalidOperationException("RabbitMQ channel creation failed.");
        }

        await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct, cancellationToken: cancellationToken);
        await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);
        await _channel.QueueBindAsync(_queueName, _exchangeName, _routingKey, cancellationToken: cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation("Received message: {Message}", message);

                var deserializedMessage = await ProcessMessageAsync<T>(message, cancellationToken);
                if (deserializedMessage != null)
                {
                    await messageProcessedHandler(deserializedMessage, cancellationToken);
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                await _channel.BasicNackAsync(ea.DeliveryTag, false, true); // Careful with requeuing!
            }
        };

        await _channel.BasicConsumeAsync(_queueName, autoAck: false, consumer: consumer);

        _logger.LogInformation("RabbitMQ consumer started.");

        await Task.Delay(Timeout.Infinite, cancellationToken); // Keep running
        _logger.LogInformation("RabbitMQ consumer stopped.");
    }

    private async Task<IChannel> CreateRabbitChannel(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ__HOST") ?? _rabbitMqConfiguration.Value.Host,
            Port = int.TryParse(Environment.GetEnvironmentVariable("RABBITMQ__PORT"), out var port) ? port : _rabbitMqConfiguration.Value.Port,
            UserName = Environment.GetEnvironmentVariable("RABBITMQ__USERNAME") ?? _rabbitMqConfiguration.Value.Username,
            Password = Environment.GetEnvironmentVariable("RABBITMQ__PASSWORD") ?? _rabbitMqConfiguration.Value.Password
        };
        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        return _channel;
    }

    private async Task<T?> ProcessMessageAsync<T>(string message, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing message: {Message}", message);

            if (string.IsNullOrEmpty(message))
            {
                _logger.LogWarning("Received null or empty message.");
                return default;
            }

            var deserializedMessage = JsonSerializer.Deserialize<T>(message);
            if (deserializedMessage == null)
            {
                _logger.LogWarning("Message deserialization failed.");
                return default;
            }

            await Task.Delay(1000, cancellationToken);
            _logger.LogInformation("Message processed: {Message}", message);

            return deserializedMessage;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Message processing cancelled.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ProcessMessageAsync: {ErrorMessage}", ex.Message);
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
        }

        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }

        GC.SuppressFinalize(this);
    }
}

