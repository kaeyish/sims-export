using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRatingRepository
    {
        public void RateGuest(Rating rating);

        public int NextId();

        public List<Rating> FindAll();

    }
}
