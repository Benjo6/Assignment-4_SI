using Assignment_4_SI.Models.API.RequestObjects;
using Assignment_4_SI.Models.API.ResponseObjects;
using Assignment_4_SI.Utility;

namespace Assignment_4_SI.Models.Entities;

public class Event : MQHappening
{
    
    public string ImageURL { get; set; }
    //public HashSet<Activity> Activities { get; set; } = new HashSet<Activity>();

    public override string ToXMLMessage()
    {
        throw new NotImplementedException();
    }

    public EventResponseObject ToResponseObject()
    {
        byte[] image = null;

        if (!String.IsNullOrEmpty(ImageURL))
        {
            using (FileStream stream = File.Open(ImageURL, FileMode.Open))
            {
                image = FileUtility.ReadFully(stream);

            }
        }
        return new EventResponseObject()
        {
            //IsActive = IsActive,
            Id = (long)Id,
            Name = Name,
            Description = Description,
            StartTime = StartTime,
            EndTime = EndTime,
            //Activities = Activities,
            ImageURL = ImageURL,
            Image = image
        };

    }

    public static Event FromCreateObject(EventCreateObject createObj)
    {
        Event e = new Event()
        {
            UUID = createObj.Name,
            Name = createObj.Name,
            Description = createObj.Description,
            StartTime = DateTime.ParseExact(createObj.StartTime,"dd/MM/yyyy HH:mm",null),
            EndTime = DateTime.ParseExact(createObj.EndTime, "dd/MM/yyyy HH:mm", null)
        };

        return e;
    }
}
