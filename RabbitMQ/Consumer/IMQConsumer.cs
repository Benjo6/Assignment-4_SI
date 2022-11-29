namespace Assignment_4_SI.RabbitMQ.Consumer;

public interface IMQConsumer
{
    Task ReceiveMessagesAsync(CancellationToken cancellationToken);
}