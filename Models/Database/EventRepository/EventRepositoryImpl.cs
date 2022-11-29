using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.XML;
using FrontEndAPI.Models.Database;

namespace Assignment_4_SI.Models.Database.EventRepository;

public class EventRepositoryImpl : IEventRepository
{
    private readonly S2ITSP2_2_Context _context;
    private readonly IXMLService _xmlService;
    
    public EventRepositoryImpl(S2ITSP2_2_Context context, IXMLService xmlService)
    {
        _context = context;
        _xmlService = xmlService;
    }
    
    public Event Create(Event e, bool fromMessage = false)
    {
        _context.Events.Add(e);
        _context.SaveChanges();

        //send xml messages
        if (!fromMessage) _xmlService.SendMessageAsync(e);

        return e;
    }
    
    public Event Get(long id)
    {
        var evt = _context.Events.FirstOrDefault(e => e.Id == id && e.IsActive);
        return evt;
    }

    public HashSet<Event> GetAll()
    {
        return _context.Events.Where(e => e.IsActive).ToHashSet<Event>();
    }

    public Event GetByName(string name)
    {
        var evt = _context.Events.FirstOrDefault(e => e.Name == name && e.IsActive);
        return evt;
    }
    
    
}