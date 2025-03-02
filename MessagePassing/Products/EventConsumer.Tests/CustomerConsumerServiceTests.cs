//using MessagePassing.Common.RabbitMq;
//using MessagePassing.Products.EventConsumer;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace EventConsumer.Tests;

//public class CustomerConsumerServiceTests
//{
//    private readonly Mock<ILogger<CustomerConsumerService>> _loggerMock;
//    private readonly Mock<IRabbitMQConsumer> _rabbitMQConsumerMock;
//    private readonly CustomerConsumerService _service;

//    public CustomerConsumerServiceTests()
//    {
//        _loggerMock = new Mock<ILogger<CustomerConsumerService>>();
//        _rabbitMQConsumerMock = new Mock<IRabbitMQConsumer>();
//        _service = new CustomerConsumerService(_loggerMock.Object, _rabbitMQConsumerMock.Object);
//    }

//    [Fact]
//    public async Task ConsumeAsync_ShouldLogStartAndEnd()
//    {
//        // Arrange
//        var cancellationToken = new CancellationToken();

//        // Act
//        await _service.ConsumeAsync(cancellationToken);

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
//    public async Task ConsumeAsync_ShouldCallStartAsyncOnRabbitMQConsumer()
//    {
//        // Arrange
//        var cancellationToken = new CancellationToken();

//        // Act
//        await _service.ConsumeAsync(cancellationToken);

//        // Assert
//        _rabbitMQConsumerMock.Verify(consumer => consumer.StartAsync(cancellationToken), Times.Once);
//    }
//}
