using EventConsumer.Infrastructure;

namespace MessagePassing.Products.EventConsumer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.Services.Configure<RabbitMqConfiguration>(
            builder.Configuration.GetSection("RabbitMqConfiguration"));

        builder.Services.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();
        builder.Services.AddScoped<ICustomerConsumerService, CustomerConsumerService>();

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}