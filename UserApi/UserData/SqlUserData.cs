using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.UserData
{
    public class SqlUserData : IUserData
    {
        private UserContext _context;
        public SqlUserData(UserContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
