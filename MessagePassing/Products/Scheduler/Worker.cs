namespace MessagePassing.Products.Scheduler;

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
            await using var scope = _serviceProvider.CreateAsyncScope();
            var scopedService = scope.ServiceProvider.GetRequiredService<ICustomerSynchronizer>();
            _logger.LogInformation("Consumer synchronization started");
            await scopedService.SynchronizeCustomerAddedAsync(cancellation);
            await Task.Delay(TimeSpan.FromSeconds(5), cancellation);
            //_logger.LogInformation("Consumer execution ended");
        }
    }
}
