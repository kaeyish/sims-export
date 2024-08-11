using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;

namespace BookingApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public User GetById(int id)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Id == id);
        }
        
        public List<User> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Update(int id, User updatedUser)
        {
            List<User> updatedUsers = new List<User>();

            foreach(var user in FindAll())
            {
                if(user.Id == id)
                {
                    updatedUsers.Add(updatedUser);
                }else
                {
                    updatedUsers.Add(user);
                }
            }

            _serializer.ToCSV(FilePath, updatedUsers);
        }

        public void PromoteToSuperOwner(int id)
        {
            User user = GetById(id);
            user.Role = "Superowner";
            
            Update(id,user);
        }

        public void DemoteFromSuperOwner(int id)
        {
            User user = GetById(id);
            user.Role = "Owner";
            
            Update(id,user);
        }


        public void PromoteToSuperGuest(int id)
        {
            User user = GetById(id);
            user.Role = "SuperGuest";

            Update(id, user);
        }

        public void DemoteFromSuperGuest(int id)
        {
            User user = GetById(id);
            user.Role = "Guest";

            Update(id, user);
        }
    }
}
