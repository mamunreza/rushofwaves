//using EventConsumer.Infrastructure;
//using MessagePassing.Products.Data;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Moq;
//using System.Text.Json;

//namespace MessagePassing.Products.EventConsumer.Tests;

//public class RabbitMQConsumerTests
//{
//    private readonly Mock<ILogger<RabbitMQConsumer>> _loggerMock;
//    private readonly Mock<IOptions<ProductsRabbitMqConfiguration>> _rabbitMqConfigMock;
//    private readonly Mock<AppDbContext> _appDbContextMock;
//    private readonly RabbitMQConsumer _rabbitMQConsumer;

//    public RabbitMQConsumerTests()
//    {
//        _loggerMock = new Mock<ILogger<RabbitMQConsumer>>();
//        _rabbitMqConfigMock = new Mock<IOptions<ProductsRabbitMqConfiguration>>();
//        _appDbContextMock = new Mock<AppDbContext>();

//        var rabbitMqConfig = new ProductsRabbitMqConfiguration
//        {
//            Host = "localhost",
//            Port = 5672,
//            Username = "guest",
//            Password = "guest",
//            Customer = new CustomerConfiguration
//            {
//                Exchange = "exchange",
//                Queue = "queue",
//                RoutingKey = "routingKey"
//            }
//        };

//        _rabbitMqConfigMock.Setup(x => x.Value).Returns(rabbitMqConfig);

//        _rabbitMQConsumer = new RabbitMQConsumer(_loggerMock.Object, _rabbitMqConfigMock.Object, _appDbContextMock.Object);
//    }

//    [Fact]
//    public async Task StartAsync_ShouldLogStartAndStop()
//    {
//        // Arrange
//        var cancellationToken = new CancellationTokenSource(1000).Token;

//        // Act
//        await _rabbitMQConsumer.StartAsync(cancellationToken);

//        // Assert
//        _loggerMock.Verify(
//            x => x.Log(
//                It.Is<LogLevel>(l => l == LogLevel.Information),
//                It.IsAny<EventId>(),
//                It.Is<It.IsAnyType>((v, t) => true),
//                It.IsAny<Exception>(),
//                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
//            Times.Exactly(2));
//    }

//    [Fact]
//    public async Task ProcessMessageAsync_ShouldLogProcessingAndProcessed()
//    {
//        // Arrange
//        var message = JsonSerializer.Serialize(new CustomerAddedInbox());
//        var cancellationToken = new CancellationToken();

//        // Act
//        await _rabbitMQConsumer.ProcessMessageAsync(message, cancellationToken);

//        // Assert
//        _loggerMock.Verify(
//            x => x.Log(
//                It.Is<LogLevel>(l => l == LogLevel.Information),
//                It.IsAny<EventId>(),
//                It.Is<It.IsAnyType>((v, t) => true),
//                It.IsAny<Exception>(),
//                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
//            Times.Exactly(2));
//    }

//    [Fact]
//    public async Task ProcessMessageAsync_ShouldAddToDbContext()
//    {
//        // Arrange
//        var message = JsonSerializer.Serialize(new CustomerAddedInbox());
//        var cancellationToken = new CancellationToken();

//        // Act
//        await _rabbitMQConsumer.ProcessMessageAsync(message, cancellationToken);

//        // Assert
//        _appDbContextMock.Verify(db => db.Add(It.IsAny<CustomerAddedInbox>()), Times.Once);
//    }

//    [Fact]
//    public async Task ProcessMessageAsync_ShouldLogErrorOnException()
//    {
//        // Arrange
//        var message = "invalid message";
//        var cancellationToken = new CancellationToken();

//        // Act
//        await Assert.ThrowsAsync<JsonException>(() => _rabbitMQConsumer.ProcessMessageAsync(message, cancellationToken));

//        // Assert
//        _loggerMock.Verify(
//            x => x.Log(
//                It.Is<LogLevel>(l => l == LogLevel.Error),
//                It.IsAny<EventId>(),
//                It.Is<It.IsAnyType>((v, t) => true),
//                It.IsAny<Exception>(),
//                It.Is<Func<It.IsAnyType, Exception?, string>>((v, t) => true)),
//            Times.Once);
//    }
//}
