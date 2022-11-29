namespace Assignment_4_SI.RabbitMQ.Configuration;

public class MqConfiguration : IMQConfiguration
{
    public string Hostname { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ExchangeName { get; set; }
    public string QueueName { get; set; }
}