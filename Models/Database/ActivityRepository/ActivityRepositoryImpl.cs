using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.XML;
using FrontEndAPI.Models.Database;

namespace Assignment_4_SI.Models.Database.ActivityRepository;

public class ActivityRepositoryImpl : IActivityRepository
{
    private readonly S2ITSP2_2_Context _context;
    private readonly IXMLService _xmlService;

    public ActivityRepositoryImpl(S2ITSP2_2_Context context, IXMLService xmlService)
    {
        _context = context;
        _xmlService = xmlService;
    }

    public HashSet<Activity> GetAll()
    {
        return _context.Activities.Where(a => a.IsActive).ToHashSet<Activity>();
        
    }

    public Activity Get(long id)
    {
        Activity act = _context.Activities.FirstOrDefault(a => a.Id == id && a.IsActive);
        return act;
        
    }

    public Activity GetByNameAndEventUUID(string name, string eventUUID)
    {
        Activity act = _context.Activities.FirstOrDefault(a => a.Event.UUID == eventUUID && a.Name == name && a.IsActive);
        return act;
        
    }

    public HashSet<Activity> GetByEventId(long eventId)
    {
        return _context.Activities.Where(a => a.EventId == eventId && a.IsActive).ToHashSet<Activity>();
        
    }

    public HashSet<Activity> GetBySpeakerId(long speakerId)
    {
        return _context.Activities.Where(a => a.SpeakerId == speakerId && a.IsActive).ToHashSet<Activity>();
        
    }

    public Activity Create(Activity a, bool fromMessage = false)
    {
        _context.Activities.Add(a);
        _context.SaveChanges();

        //send xml messages
        if (!fromMessage) _xmlService.SendMessageAsync(a);

        return a;
        
    }

    public Activity Update(Activity a, bool fromMessage = false)
    {
        throw new NotImplementedException();
    }
}