namespace EventConsumer.Infrastructure;

public class RabbitMqConfiguration
{
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required CustomerConfiguration Customer { get; set; }
}

public class CustomerConfiguration
{
    public required string Exchange { get; set; }
    public required string Queue { get; set; }
    public required string RoutingKey { get; set; }
}
