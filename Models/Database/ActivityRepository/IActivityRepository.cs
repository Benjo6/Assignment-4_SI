using Assignment_4_SI.Models.Entities;

namespace Assignment_4_SI.Models.Database.ActivityRepository;

public interface IActivityRepository
{
    HashSet<Activity> GetAll();
    Activity Get(long id);
    Activity GetByNameAndEventUUID(string name, string eventUUID);
    HashSet<Activity> GetByEventId(long eventId);
    HashSet<Activity> GetBySpeakerId(long speakerId);
    Activity Create(Activity a,bool fromMessage=false);
    Activity Update(Activity a,bool fromMessage=false);
    //void Delete(Activity a,bool fromMessage = false);
}