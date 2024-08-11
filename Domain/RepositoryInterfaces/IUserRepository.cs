using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public User GetByUsername(string username);

        public User GetById(int id);

        public List<User> FindAll();

        public void Update(int id, User updatedUser);

        public void PromoteToSuperOwner(int id);

        public void DemoteFromSuperOwner(int id);

        public void PromoteToSuperGuest(int id);

        public void DemoteFromSuperGuest(int id);
    }
}
