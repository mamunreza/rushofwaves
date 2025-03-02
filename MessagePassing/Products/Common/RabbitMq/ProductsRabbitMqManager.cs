using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MessagePassing.Common.RabbitMq;

public class ProductsRabbitMqManager
{
    private readonly IOptions<RabbitMqConfiguration> _rabbitMqConfiguration;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

    public ProductsRabbitMqManager(IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration;
    }

    public async Task<IChannel?> GetChannelAsync(CancellationToken cancellationToken)
    {
        return await CreateRabbitMqChannel(cancellationToken);
    }

    private async Task<IChannel> CreateRabbitMqChannel(CancellationToken cancellationToken)
    {
        if (_channel != null)
        {
            return _channel;
        }

        await _semaphoreSlim.WaitAsync(cancellationToken);
        try
        {
            if (_channel == null)
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
            }
        }
        finally
        {
            _semaphoreSlim.Release();
        }

        return _channel;
    }
}
