using MessagePassing.Products.Data;
using MessagePassing.Products.Scheduler.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MessagePassing.Products.Scheduler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        builder.Services.AddDbContext<IAppDbContext, AppDbContext>(opt =>
        {
            string? connectionString = GetConnectionString(builder);
            if (!string.IsNullOrEmpty(connectionString))
            {
                opt.UseNpgsql(connectionString);
            }
        });
        builder.Services.AddScoped<ICustomerSynchronizer, CustomerSynchronizer>();
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