using EventPublisher.Infrastructure;
using MessagePassing.Products.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    string? connectionString = GetConnectionString(builder);
    if (!string.IsNullOrEmpty(connectionString))
    {
        opt.UseNpgsql(connectionString);
    }
});
builder.Services.AddScoped<IDesignTimeDbContextFactory<AppDbContext>, AppDbContextFactory>();
builder.Services.AddScoped<IMessagePublishRetryPolicy, MessagePublishRetryPolicy>();
builder.Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddScoped<IProductsEventService, ProductsEventService>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    //var context = factory.CreateDbContext(Array.Empty<string>());
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
    throw;
}

await app.RunAsync();

static string? GetConnectionString(HostApplicationBuilder builder)
{
    var postgreSqlConfig = builder.Configuration.GetSection("PostgreSql").Get<PostgreSqlConfiguration>();
    var host = Environment.GetEnvironmentVariable("POSTGRESQL__HOST") ?? postgreSqlConfig.Host;
    var port = Environment.GetEnvironmentVariable("POSTGRESQL__PORT") ?? postgreSqlConfig.Port.ToString();
    var database = Environment.GetEnvironmentVariable("POSTGRESQL__DATABASE") ?? postgreSqlConfig.Database;
    var user = Environment.GetEnvironmentVariable("POSTGRESQL__USER") ?? postgreSqlConfig.User;
    var password = Environment.GetEnvironmentVariable("POSTGRESQL__PASSWORD") ?? postgreSqlConfig.Password;

    return $"Host={host};Port={port};Database={database};Username={user};Password={password}";
}