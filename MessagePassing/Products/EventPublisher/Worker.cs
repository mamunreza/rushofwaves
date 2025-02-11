namespace MessagePassing.Products.EventPublisher;

public class Worker : BackgroundService
{
    //private readonly IProductsEventService _productsEventService;
    private readonly IServiceProvider _serviceProvider;

    public Worker(
        //IProductsEventService productsEventService,
        IServiceProvider serviceProvider)
    {
        //_productsEventService = productsEventService;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var scopedService = scope.ServiceProvider.GetRequiredService<IProductsEventService>();
            await scopedService.ProcessEventsAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

            //var product = FakeProductProducer.ProduceRandomProduct();
            //var productJson = System.Text.Json.JsonSerializer.Serialize(product);
            //await _productsEventService.ProcessEventsAsync(stoppingToken);
            // Delay to simulate periodic publishing
            //await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}
