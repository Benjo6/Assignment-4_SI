namespace Assignment_4_SI.Models.Entities;

public abstract class MQEntity
{
    public long? Id { get; set; } 
    public string UUID { get; set; } 
    public long Version { get; set; } = 1;
    public bool IsActive { get; set; } = true;
    public DateTime LastUpdated { get; set; }

    public abstract string ToXMLMessage();
}