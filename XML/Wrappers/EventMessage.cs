using System.Xml.Serialization;
using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.Utility;

namespace Assignment_4_SI.XML.Wrappers;

[XmlRoot(ElementName = "message", Namespace = "http://s2it.ehb.be/messages")]
public class EventMessage
{
    private static readonly string XMLNS = "http://s2it.ehb.be/messages";

    private static readonly string SCHEMALOCATION =
        "http://s2it.ehb.be/messages http://dtsw.ehb.be/~jan.moriaux/xml/EventMessage.xsd";

    private static readonly string TYPEID = "2";
    private static readonly string SENDERUUID = "0a6cca47-af4c-41d8-a3ed-86ff5a00f388";

    [XmlElement(ElementName = "uuid", Namespace = "http://s2it.ehb.be/messages")]
    public string Uuid { get; set; }

    [XmlElement(ElementName = "version", Namespace = "http://s2it.ehb.be/messages")]
    public string Version { get; set; }

    [XmlElement(ElementName = "isActive", Namespace = "http://s2it.ehb.be/messages")]
    public string IsActive { get; set; }

    [XmlElement(ElementName = "name", Namespace = "http://s2it.ehb.be/messages")]
    public string Name { get; set; }

    [XmlElement(ElementName = "description", Namespace = "http://s2it.ehb.be/messages")]
    public string Description { get; set; }

    [XmlElement(ElementName = "startTime", Namespace = "http://s2it.ehb.be/messages")]
    public string StartTime { get; set; }

    [XmlElement(ElementName = "endTime", Namespace = "http://s2it.ehb.be/messages")]
    public string EndTime { get; set; }

    [XmlAttribute(AttributeName = "xmlns")]
    public string Xmlns { get; set; }

    [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public string SchemaLocation { get; set; }

    [XmlAttribute(AttributeName = "typeId")]
    public string TypeId { get; set; }

    [XmlAttribute(AttributeName = "timestamp")]
    public string Timestamp { get; set; }

    [XmlAttribute(AttributeName = "senderUUID")]
    public string SenderUUID { get; set; }

    public static EventMessage FromEvent(Event e)
    {
        return new EventMessage
        {
            Uuid = e.UUID,
            Version = e.Version.ToString().ToLower(),
            IsActive = e.IsActive.ToString().ToLower(),
            Name = e.Name,
            Description = e.Description,
            StartTime = DateUtility.ConvertToStringWithZoneOffset(e.StartTime),
            EndTime = DateUtility.ConvertToStringWithZoneOffset(e.EndTime),
            Xmlns = XMLNS,
            SchemaLocation = SCHEMALOCATION,
            TypeId = TYPEID,
            Timestamp = DateUtility.ConvertToStringWithZoneOffset(DateTime.Now.ToLocalTime()),
            SenderUUID = SENDERUUID
        };
    }
}