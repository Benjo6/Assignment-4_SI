namespace Assignment_4_SI.Configuration;

public interface ISystemConfiguration
{
    string FrontEnd { get; set; }
    string CashRegister { get; set; }
    string Facturation { get; set; }
    string CRM { get; set; }
}