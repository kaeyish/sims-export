using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IOwnerRatingRepository
    {
        public OwnerRating Save(OwnerRating OwnerRating);

        public List<OwnerRating> FindAll();

        public int Count(int ownerId);

        public List<OwnerRating> FindAllByOwnerId(int ownerId);

        public int NextId();

        public bool CheckReservationId(int accommodationReservationId);
    }
}
