using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.Repository
{
    public class OwnerRatingRepository : IOwnerRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/ownerRatings.csv";

        private readonly Serializer<OwnerRating> _serializer;

        private List<OwnerRating> _ownerRatings;
        
        public OwnerRatingRepository()
        {
            _serializer = new Serializer<OwnerRating>();
            _ownerRatings = _serializer.FromCSV(FilePath);
        }

        public OwnerRating Save(OwnerRating OwnerRating)
        {
            OwnerRating.Id = NextId();
            _ownerRatings = _serializer.FromCSV(FilePath);
            _ownerRatings.Add(OwnerRating);
            _serializer.ToCSV(FilePath, _ownerRatings);
            return OwnerRating;

        }

        public List<OwnerRating> FindAll()
        {
            return _serializer.FromCSV(FilePath);

        }

        public int Count(int ownerId)
        {
            return _serializer.FromCSV(FilePath).Select(r => r.Id = ownerId).Count();
        }

        public List<OwnerRating> FindAllByOwnerId(int ownerId)
        {
            List<OwnerRating> ownerRatings = new List<OwnerRating>();

            foreach(var ownerRating in _serializer.FromCSV(FilePath))
            {
                if(ownerRating.OwnerId == ownerId)
                {
                    ownerRatings.Add(ownerRating);
                }
            }

            return ownerRatings;
        }

        public int NextId()
        {
            _ownerRatings = _serializer.FromCSV(FilePath);
            if (_ownerRatings.Count < 1)
            {
                return 1;
            }
            return _ownerRatings.Max(a => a.Id) + 1;
        }

        public bool CheckReservationId(int accommodationReservationId)
        {
            List<OwnerRating> ownerRatings = new List<OwnerRating>();

            foreach (var ownerRating in _serializer.FromCSV(FilePath))
            {
                if (ownerRating.AccommodationReservationId == accommodationReservationId)
                {
                    return true;
                }
            }

            return false;

        }
    }
}
