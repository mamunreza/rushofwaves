using EventConsumer;

namespace MessagePassing.Products.EventConsumer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();
        builder.Services.AddScoped<ICustomerConsumerService, CustomerConsumerService>();

        var host = builder.Build();
        host.Run();
    }
}