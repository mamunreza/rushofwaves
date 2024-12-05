using EventPublisher;

public class Worker : BackgroundService
{
    private readonly IProductsEventService _productsEventService;

    public Worker(IProductsEventService productsEventService)
    {
        _productsEventService = productsEventService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int counter = 0;

        while (!stoppingToken.IsCancellationRequested)
        {
            var message = $"Message {++counter} sent at {DateTime.UtcNow}";
            await _productsEventService.PublishMessageAsync(message);

            // Delay to simulate periodic publishing
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}
