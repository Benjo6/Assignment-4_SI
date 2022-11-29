using Assignment_4_SI.Models.Entities;

namespace Assignment_4_SI.XML;

public interface IXMLService
{
    Task SendMessageAsync(MQEntity entity);
    bool ValidateXML(string xml);
    XMLService.MessageType? GetMessageTypeFromXML(string xml);
    string GetSenderUUIDFromXML(string xml);
    MQEntity GenerateEntityFromMessage(string xml);
}