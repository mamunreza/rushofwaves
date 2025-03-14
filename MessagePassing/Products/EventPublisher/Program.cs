using MessagePassing.Products.Data;
using MessagePassing.Products.EventPublisher;
using MessagePassing.Products.EventPublisher.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.Services.Configure<RabbitMqConfiguration>(
            builder.Configuration.GetSection("RabbitMqConfiguration"));

        builder.Services.AddDbContext<IAppDbContext, AppDbContext>(opt =>
        {
            string? connectionString = GetConnectionString(builder);
            if (!string.IsNullOrEmpty(connectionString))
            {
                opt.UseNpgsql(connectionString);
            }
        });
        //builder.Services.AddScoped<IDesignTimeDbContextFactory<AppDbContext>, AppDbContextFactory>();
        builder.Services.AddScoped<IMessagePublishRetryPolicy, MessagePublishRetryPolicy>();
        builder.Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
        builder.Services.AddScoped<IProductsEventService, ProductsEventService>();
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }

    static string? GetConnectionString(HostApplicationBuilder builder)
    {
        var postgreSqlConfig = builder.Configuration.GetSection("PostgreSql").Get<PostgreSqlConfiguration>();
        var host = Environment.GetEnvironmentVariable("POSTGRESQL__HOST") ?? postgreSqlConfig.Host;
        var port = Environment.GetEnvironmentVariable("POSTGRESQL__PORT") ?? postgreSqlConfig.Port.ToString();
        var database = Environment.GetEnvironmentVariable("POSTGRESQL__DATABASE") ?? postgreSqlConfig.Database;
        var user = Environment.GetEnvironmentVariable("POSTGRESQL__USERNAME") ?? postgreSqlConfig.Username;
        var password = Environment.GetEnvironmentVariable("POSTGRESQL__PASSWORD") ?? postgreSqlConfig.Password;

        return $"Host={host};Port={port};Database={database};Username={user};Password={password}";
    }
}




//HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

//// Add configuration from appsettings.json
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//builder.Services.Configure<RabbitMqConfiguration>(
//    builder.Configuration.GetSection("RabbitMqConfiguration"));

//builder.Services.AddDbContext<AppDbContext>(opt =>
//{
//    string? connectionString = GetConnectionString(builder);
//    if (!string.IsNullOrEmpty(connectionString))
//    {
//        opt.UseNpgsql(connectionString);
//    }
//});
////builder.Services.AddScoped<IDesignTimeDbContextFactory<AppDbContext>, AppDbContextFactory>();
//builder.Services.AddScoped<IMessagePublishRetryPolicy, MessagePublishRetryPolicy>();
//builder.Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
//builder.Services.AddScoped<IProductsEventService, ProductsEventService>();
//builder.Services.AddHostedService<Worker>();

//var app = builder.Build();

////using var scope = app.Services.CreateScope();
////var services = scope.ServiceProvider;
////try
////{
////    var context = services.GetRequiredService<AppDbContext>();
////    //var context = factory.CreateDbContext(Array.Empty<string>());
////    await context.Database.MigrateAsync();
////}
////catch (Exception ex)
////{
////    var logger = services.GetRequiredService<ILogger<Program>>();
////    logger.LogError(ex, "An error occurred during migration");
////    throw;
////}

//await app.RunAsync();

//static string? GetConnectionString(HostApplicationBuilder builder)
//{
//    var postgreSqlConfig = builder.Configuration.GetSection("PostgreSql").Get<PostgreSqlConfiguration>();
//    var host = Environment.GetEnvironmentVariable("POSTGRESQL__HOST") ?? postgreSqlConfig.Host;
//    var port = Environment.GetEnvironmentVariable("POSTGRESQL__PORT") ?? postgreSqlConfig.Port.ToString();
//    var database = Environment.GetEnvironmentVariable("POSTGRESQL__DATABASE") ?? postgreSqlConfig.Database;
//    var user = Environment.GetEnvironmentVariable("POSTGRESQL__USERNAME") ?? postgreSqlConfig.Username;
//    var password = Environment.GetEnvironmentVariable("POSTGRESQL__PASSWORD") ?? postgreSqlConfig.Password;

//    return $"Host={host};Port={port};Database={database};Username={user};Password={password}";
//}