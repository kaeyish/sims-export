using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ISuperGuestRepository
    {

        public SuperGuest Save(SuperGuest superGuest);

        public List<SuperGuest> FindAll();

        public SuperGuest FindByGuestId(int guestId);

        public int NextId();

        public void DeleteByGuestId(int guestId);

        public void Update(SuperGuest superGuest);



    }
}
