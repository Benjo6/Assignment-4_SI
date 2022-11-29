namespace Assignment_4_SI.Models.Entities;

public abstract class MQHappening : MQEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}