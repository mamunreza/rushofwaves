using MessagePassing.Products.API.Services;
using MessagePassing.Products.API.Infrastructure;
using MessagePassing.Products.Data;
using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<IAppDbContext, AppDbContext>(opt =>
        {
            string? connectionString = GetConnectionString(builder);
            if (!string.IsNullOrEmpty(connectionString))
            {
                opt.UseNpgsql(connectionString);
            }
        });
        builder.Services.AddScoped<IProductService, ProductService>();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
            throw;
        }

        await app.RunAsync();
    }

    static string? GetConnectionString(WebApplicationBuilder builder)
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
