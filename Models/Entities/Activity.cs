 using Assignment_4_SI.Models.API.RequestObjects;
 using Assignment_4_SI.Models.API.ResponseObjects;
 using Assignment_4_SI.Utility;

 namespace Assignment_4_SI.Models.Entities;

public class Activity : MQHappening
{
    public Event Event { get; set; }   
    public User Speaker { get; set; }
    public long EventId { get; set; }
    public long? SpeakerId { get; set; }
    public decimal Price { get; set; }
    public int RemainingCapacity { get; set; }
    public override string ToXMLMessage()
    {
        throw new NotImplementedException();
    }

    public ActivityResponseObject ToResponseObject()
    {
        return new ActivityResponseObject()
        {
            //IsActive = IsActive,
            Id = (long)Id,
            Name = Name,
            Description = Description,
            StartTime = StartTime,
            EndTime = EndTime,
            EventId = EventId,
            SpeakerId = SpeakerId,
            Price = Price,
            RemainingCapacity = RemainingCapacity
        };
    }

    public static Activity FromCreateObjectTuple(Tuple<ActivityCreateObject, string> createObjTuple)
    {
        var createObj = createObjTuple.Item1;
        var eventUUID = createObjTuple.Item2;

        Activity a = new Activity()
        {
            UUID = UUIDGenerator.GenerateActivityUUID(createObj.Name, eventUUID),
            Name = createObj.Name,
            Description = createObj.Description,
            StartTime = DateTime.ParseExact(createObj.StartTime, "dd/MM/yyyy HH:mm", null),
            EndTime = DateTime.ParseExact(createObj.EndTime, "dd/MM/yyyy HH:mm", null),
            EventId = createObj.EventId,
            SpeakerId = createObj.SpeakerId,
            Price = createObj.Price,
            RemainingCapacity = createObj.RemainingCapacity
        };

        return a;
    }
}