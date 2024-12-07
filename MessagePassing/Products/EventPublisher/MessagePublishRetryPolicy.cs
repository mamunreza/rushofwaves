using Polly;
using Polly.Retry;

public interface IMessagePublishRetryPolicy
{
    AsyncRetryPolicy ApplyAsync();
}

public class MessagePublishRetryPolicy(
    ILogger<RabbitMQPublisher> logger) : IMessagePublishRetryPolicy
{
    private readonly ILogger<RabbitMQPublisher> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    public AsyncRetryPolicy ApplyAsync()
    {
        return Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    _logger.LogWarning(
                        "Retry {RetryCount} encountered an error: {Message}. Waiting {TimeSpan} before next retry.",
                        retryCount, exception.Message, timeSpan);
                });
    }
}