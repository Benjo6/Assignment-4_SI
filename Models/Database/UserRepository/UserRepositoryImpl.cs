using Assignment_4_SI.Models.Entities;
using Assignment_4_SI.XML;
using FrontEndAPI.Models.Database;

namespace Assignment_4_SI.Models.Database.UserRepository;

public class UserRepositoryImpl : IUserRepository
{
    private readonly S2ITSP2_2_Context _context;
    private readonly IXMLService _xmlService;

    public UserRepositoryImpl(S2ITSP2_2_Context context, IXMLService xmlService)
    {
        _context = context;
        _xmlService = xmlService;
    }

    public HashSet<User> GetAll()
    {
        return _context.Users.Where(u => u.IsActive).ToHashSet<User>();

    }

    public User Get(long id)
    {
        User u = _context.Users.FirstOrDefault(user => user.Id == id && user.IsActive);
        return u;

    }

    public User GetByEmail(string email)
    {
        User u = _context.Users.FirstOrDefault(user => user.Email.ToLower() == email.ToLower() && user.IsActive);
        return u;

    }

    public User Create(User u, bool fromMessage = false)
    {
        _context.Users.Add(u);
        _context.SaveChanges();

        //send xml messages
        if (!fromMessage) _xmlService.SendMessageAsync(u);

        return u;

    }

    public User Update(User u, bool fromMessage = false)
    {
        u.Version++;
        _context.Users.Update(u);
        _context.SaveChanges();

        //send xml messages
        if (!fromMessage) _xmlService.SendMessageAsync(u);

        return u;

    }

    public void Delete(User u, bool fromMessage = false)
    {
        {
            //deactivate user
            u.Version++;
            u.IsActive = false;
            _context.Users.Update(u);

            //deactivate related activities for speakers
            var acts = _context.Activities.Where(a => a.SpeakerId == u.Id).ToList<Activity>();
            var activityReservations = new List<Reservation>();
            foreach (var a in acts)
            {
                a.Version++;
                a.IsActive = false;
                _context.Activities.Update(a);
                //deactivate the activity's reservations
                activityReservations = _context.Reservations.Where(r => r.ActivityId == a.Id && r.IsActive)
                    .ToList<Reservation>();
                foreach (var r in activityReservations)
                {
                    r.Version++;
                    r.IsActive = false;
                    _context.Reservations.Update(r);
                }
            }

            //deactivate the user's reservations
            var ress = _context.Reservations.Where(r => r.VisitorId == u.Id && !activityReservations.Contains(r))
                .ToList<Reservation>();
            foreach (var r in ress)
            {
                r.Version++;
                r.IsActive = false;
                _context.Reservations.Update(r);
            }

            _context.SaveChanges();

            //send xml messages: 
            //don't send user message if user has been deleted by message
            if (!fromMessage)
            {
                _xmlService.SendMessageAsync(u);

            }

            //send updated dependent activites and reservations
            foreach (var a in acts)
            {
                _xmlService.SendMessageAsync(a);
            }

            foreach (var r in activityReservations)
            {
                _xmlService.SendMessageAsync(r);
            }

            foreach (var r in ress)
            {
                _xmlService.SendMessageAsync(r);
            }
        }
    }
}
