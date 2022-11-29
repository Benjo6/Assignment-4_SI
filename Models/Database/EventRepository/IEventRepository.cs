using Assignment_4_SI.Models.Entities;

namespace Assignment_4_SI.Models.Database.EventRepository;

public interface IEventRepository
{
    HashSet<Event> GetAll();
    Event Get(long id);
    Event GetByName(string name);
    Event Create(Event e, bool fromMessage = false);
}