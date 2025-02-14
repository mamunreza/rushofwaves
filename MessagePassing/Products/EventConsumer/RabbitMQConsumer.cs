using EventConsumer.Infrastructure;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public interface IRabbitMQConsumer
{
    Task StartAsync(CancellationToken cancellationToken);
}

public class RabbitMQConsumer : IRabbitMQConsumer, IAsyncDisposable
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly string _exchangeName = "customer.exchange";
    private readonly string _queueName = "customer.queue";
    private readonly string _routingKey = "customer";
    private readonly ILogger<RabbitMQConsumer> _logger;
    private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;
    private IChannel _channel;
    private IConnection _connection;

    public RabbitMQConsumer(
        //string host, int port, string username, string password, string exchangeName, string queueName, string routingKey, 
        ILogger<RabbitMQConsumer> logger,
        IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _rabbitMqConfiguration = rabbitMqConfiguration
            ?? throw new ArgumentNullException(nameof(rabbitMqConfiguration));

        //_connectionFactory = new ConnectionFactory()
        //{
        //    HostName = Environment.GetEnvironmentVariable("RABBITMQ__HOST") ?? _rabbitMqConfiguration.Value.Host,
        //    Port = int.TryParse(Environment.GetEnvironmentVariable("RABBITMQ__PORT"), out var port) ? port : _rabbitMqConfiguration.Value.Port,
        //    UserName = Environment.GetEnvironmentVariable("RABBITMQ__USERNAME") ?? _rabbitMqConfiguration.Value.Username,
        //    Password = Environment.GetEnvironmentVariable("RABBITMQ__PASSWORD") ?? _rabbitMqConfiguration.Value.Password
        //};
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
        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Direct);
        await _channel.QueueDeclareAsync(_queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        await _channel.QueueBindAsync(_queueName, _exchangeName, _routingKey);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Received message: {message}");

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

    private async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Processing message: {message}");
            await Task.Delay(1000, cancellationToken); // Your logic here
            _logger.LogInformation($"Message processed: {message}");
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Message processing cancelled.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in ProcessMessageAsync: {ex.Message}");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        _channel?.CloseAsync();
        _connection?.CloseAsync();
        await Task.CompletedTask;
    }
}
