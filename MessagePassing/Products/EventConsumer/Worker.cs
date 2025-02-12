namespace MessagePassing.Products.EventConsumer;

public class Worker(
    ILogger<Worker> logger,
    IServiceProvider serviceProvider) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger 
        ?? throw new ArgumentNullException(nameof(logger));
    private readonly IServiceProvider _serviceProvider = serviceProvider
        ?? throw new ArgumentNullException(nameof(serviceProvider));

    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        while (!cancellation.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var scopedService = scope.ServiceProvider.GetRequiredService<ICustomerConsumerService>();
            _logger.LogInformation("Consumer execution started");
            await scopedService.ConsumeAsync(cancellation);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellation);
            _logger.LogInformation("Consumer execution ended");
        }
    }
}
