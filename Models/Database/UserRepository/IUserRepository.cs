using Assignment_4_SI.Models.Entities;

namespace Assignment_4_SI.Models.Database.UserRepository;

public interface IUserRepository
{
    HashSet<User> GetAll();
    User Get(long id);
    User GetByEmail(string email);
    User Create(User u,bool fromMessage = false);
    User Update(User u,bool fromMessage = false);
    void Delete(User u,bool fromMessage =false);
}