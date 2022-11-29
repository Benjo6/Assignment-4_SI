namespace Assignment_4_SI.Models.Database.UUIDRepository;

public interface IUUIDRepository
{
    string GetActivityUUIDFromId(long id);
    string GetEventUUIDFromId(long id);
    string GetUserUUIDFromId(long id);
    long? GetEventIdFromUUID(string UUID);
    long? GetActivityIdFromUUID(string UUID);
    long? GetUserIdFromUUID(string UUID);
    long? GetReservationIdFromUUID(string UUID);
}