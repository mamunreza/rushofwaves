
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
            // var message = $"Message {++counter} sent at {DateTime.UtcNow}";
            var product = FakeProductProducer.ProduceRandomProduct();
            var productJson = System.Text.Json.JsonSerializer.Serialize(product);
            await _productsEventService.PublishMessageAsync(productJson);

            // Delay to simulate periodic publishing
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
