using UserApi.Models;

namespace UserApi.UserData
{
    public class MockUserData : IUserData
    {
        private List<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Name1",
                LastName = "Last1",
                DateOfBirth = System.DateTime.Now
            },

            new User()
            {
                Id = 2,
                FirstName = "Name2",
                LastName = "Last2",
                DateOfBirth = System.DateTime.Now
            },

            new User()
            {
                Id = 3,
                FirstName = "Name3",
                LastName = "Last3",
                DateOfBirth = System.DateTime.Now
            },
        };
        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        public User GetUser(int id)
        {
            return users.SingleOrDefault(x => x.Id == id);
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public User UpdateUser(User user)
        {
            var existingUser = GetUser(user.Id);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.DateOfBirth = user.DateOfBirth;
            return existingUser;
        }
    }
}
