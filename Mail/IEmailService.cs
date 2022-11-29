using Assignment_4_SI.Models.Entities;

namespace Assignment_4_SI.Mail;

public interface IEmailService
{
    
    void Send(EmailMessage emailMessage);
    void SendRegistrationConfirmation(Tuple<string,User> userAndOriginalPassword);
    void SendPasswordResetMail(Tuple<string, User> userAndOriginalPassword);
    void SendReservationConfirmation(Tuple<User, Activity, Event,bool> userAndActivityAndEventAndWithInvoice);
}