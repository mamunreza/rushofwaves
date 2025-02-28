using EventConsumer.Infrastructure;
using MessagePassing.Products.Data;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MessagePassing.Products.EventConsumer;

public interface IRabbitMQConsumer
{
    Task StartAsync(CancellationToken cancellationToken);
}

public class RabbitMQConsumer : IRabbitMQConsumer, IAsyncDisposable
{
    private readonly ILogger<RabbitMQConsumer> _logger;
    private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;
    private readonly AppDbContext _appDbContext;

    private readonly string _exchangeName;
    private readonly string _queueName;
    private readonly string _routingKey;
    private IChannel? _channel;
    private IConnection? _connection;

    public RabbitMQConsumer(
        ILogger<RabbitMQConsumer> logger, 
        IOptions<RabbitMqConfiguration> rabbitMqConfiguration,
        AppDbContext appDbContext)
    {
        _logger = logger 
            ?? throw new ArgumentNullException(nameof(logger));
        _rabbitMqConfiguration = rabbitMqConfiguration 
            ?? throw new ArgumentNullException(nameof(rabbitMqConfiguration));
        _appDbContext = appDbContext
            ?? throw new ArgumentNullException(nameof(appDbContext));

        _exchangeName = _rabbitMqConfiguration.Value.Customer.Exchange;
        _queueName = _rabbitMqConfiguration.Value.Customer.Queue;
        _routingKey = _rabbitMqConfiguration.Value.Customer.RoutingKey;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
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

                await ProcessMessageAsync(message, cancellationToken);

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

    internal async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Processing message: {Message}", message);

            var customerAddedInboxItem = JsonSerializer.Deserialize<CustomerAddedInbox>(message);
            if (customerAddedInboxItem == null)
            {
                _logger.LogWarning("Message deserialization failed.");
                return;
            }

            _appDbContext.Add(customerAddedInboxItem);
            await Task.Delay(1000, cancellationToken);
            _logger.LogInformation("Message processed: {Message}", message);
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
