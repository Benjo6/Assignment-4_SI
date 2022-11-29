using Assignment_4_SI.Models.Entities;

namespace Assignment_4_SI.Models.Database.ReservationRepository;

public interface IReservationRepository
{
    HashSet<Reservation> GetAll();
    Reservation Get(long id);
    Reservation GetByActivityIdAndVisitorId(long activityId, long visitorId);
    HashSet<Reservation> GetByActivityId(long activityId);
    HashSet<Reservation> GetByVisitorId(long visitorId);
    Reservation Create(Reservation r,bool fromMessage = false);
    void Delete(Reservation r,bool fromMessage = false);
    Reservation Update(Reservation r, bool fromMessage = false);
}