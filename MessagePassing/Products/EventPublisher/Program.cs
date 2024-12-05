using EventPublisher;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IMessagePublishRetryPolicy, MessagePublishRetryPolicy>();
builder.Services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();
builder.Services.AddSingleton<IProductsEventService, ProductsEventService>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();
await app.RunAsync();
