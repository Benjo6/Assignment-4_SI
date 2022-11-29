using FrontEndAPI.Models.Database;

namespace Assignment_4_SI.Models.Database.UUIDRepository;

public class UUIDRepositoryImpl:IUUIDRepository
{
    private readonly S2ITSP2_2_Context _context;

    public UUIDRepositoryImpl(S2ITSP2_2_Context context)
    {
        _context = context;
    }

    public string GetActivityUUIDFromId(long id)
    {
        return _context.Activities.FirstOrDefault(a => a.Id == id)?.UUID;
        
    }

    public string GetEventUUIDFromId(long id)
    {
        return _context.Events.FirstOrDefault(e => e.Id == id)?.UUID;
        
    }

    public string GetUserUUIDFromId(long id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id)?.UUID;
        
    }

    public long? GetEventIdFromUUID(string UUID)
    {
        return (long?)_context.Events.FirstOrDefault(e => e.UUID == UUID)?.Id;
        
    }

    public long? GetActivityIdFromUUID(string UUID)
    {
        return (long?)_context.Activities.FirstOrDefault(a => a.UUID == UUID)?.Id;
        
    }

    public long? GetUserIdFromUUID(string UUID)
    {
        return (long?)_context.Users.FirstOrDefault(u => u.UUID == UUID)?.Id;
    }

    public long? GetReservationIdFromUUID(string UUID)
    {
        return (long?)_context.Reservations.FirstOrDefault(r => r.UUID == UUID)?.Id;
        
    }
}