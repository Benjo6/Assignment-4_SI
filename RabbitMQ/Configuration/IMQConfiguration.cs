namespace Assignment_4_SI.RabbitMQ.Configuration;

public interface IMQConfiguration
{
    string Hostname { get; set; }
    int Port { get; set; }
    string Username { get; set; }
    string Password { get; set; }
    string ExchangeName { get; set; }
    string QueueName { get; set; }
}