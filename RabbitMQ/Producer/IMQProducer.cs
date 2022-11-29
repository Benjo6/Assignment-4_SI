namespace Assignment_4_SI.RabbitMQ.Producer;

public interface IMQProducer
{
    Task SendMessageAsync(string message);
}