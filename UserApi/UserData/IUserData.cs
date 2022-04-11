using UserApi.Models;

namespace UserApi.UserData
{
    public interface IUserData
    {
        List<User> GetUsers();
        User GetUser(int id);
        void AddUser(User user);
        void DeleteUser(User user);
        User UpdateUser(User user);
    }
}
