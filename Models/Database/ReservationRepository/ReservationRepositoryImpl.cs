using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.XML;
using FrontEndAPI.Models.Database;

namespace Assignment_4_SI.Models.Database.ReservationRepository;

public class ReservationRepositoryImpl : IReservationRepository
{
    private readonly S2ITSP2_2_Context _context;
    private readonly IXMLService _xmlService;

    public ReservationRepositoryImpl(S2ITSP2_2_Context context, IXMLService xmlService)
    {
        _context = context;
        _xmlService = xmlService;
    }

    public HashSet<Reservation> GetAll()
    {
        return _context.Reservations.Where(r => r.IsActive).ToHashSet<Reservation>();
        
    }

    public Reservation Get(long id)
    {
        return _context.Reservations.FirstOrDefault(r => r.Id == id && r.IsActive);
        
    }

    public Reservation GetByActivityIdAndVisitorId(long activityId, long visitorId)
    {
        return _context.Reservations.FirstOrDefault(r => r.ActivityId == activityId && r.VisitorId == visitorId && r.IsActive);
        
    }

    public HashSet<Reservation> GetByActivityId(long activityId)
    {
        return _context.Reservations.Where(r => r.ActivityId == activityId && r.IsActive).ToHashSet<Reservation>();
    }

    public HashSet<Reservation> GetByVisitorId(long visitorId)
    {
        return _context.Reservations.Where(r => r.VisitorId == visitorId && r.IsActive).ToHashSet<Reservation>();
        
    }

    public Reservation Create(Reservation r, bool fromMessage = false)
    { 
        //add the reservation
        _context.Reservations.Add(r);
        //update the related activity's capacity
        var act = _context.Activities.FirstOrDefault(a => a.Id == r.ActivityId);
        act.Version++;
        act.RemainingCapacity--;
        _context.Activities.Update(act);

        _context.SaveChanges();

        //send xml messages
        //if reservation created from message, don't repost the reservation to queue
        if (!fromMessage) _xmlService.SendMessageAsync(r);
        //post the activity with updated capacity to queue
        _xmlService.SendMessageAsync(act);

        return r;
    }

    public void Delete(Reservation r, bool fromMessage = false)
    { 
        //deactivate the reservation
        r.IsActive = false;
        r.Version++;
        _context.Reservations.Update(r);
        //update the related activity's capacity
        var act = _context.Activities.FirstOrDefault(a => a.Id == r.ActivityId);
        act.Version++;
        act.RemainingCapacity++;
        _context.Activities.Update(act);

        _context.SaveChanges();

        //send xml messages
        //if reservation deleted from message, don't repost the reservation to queue
        if (!fromMessage) _xmlService.SendMessageAsync(r);
        //post the activity with updated capacity to queue
        _xmlService.SendMessageAsync(act);
        
    }

    public Reservation Update(Reservation r, bool fromMessage = false)
    {
        r.Version++;
        _context.Reservations.Update(r);
        _context.SaveChanges();
        if (!fromMessage) _xmlService.SendMessageAsync(r);

        return r;    
    }
}