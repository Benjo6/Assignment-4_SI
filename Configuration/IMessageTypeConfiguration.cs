namespace Assignment_4_SI.Configuration;

public interface IMessageTypeConfiguration
{
    int User { get; set; }
    int Event { get; set; }
    int Activity { get; set; }
    int Reservation {get;set;}

}