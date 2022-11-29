namespace Assignment_4_SI.Configuration;

public class MessageTypeConfiguration : IMessageTypeConfiguration
{
    public int User { get; set; }
    public int Event { get; set; }
    public int Activity { get; set; }
    public int Reservation { get; set; }
}