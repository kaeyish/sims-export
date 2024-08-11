using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class RatingService
    {
        private IRatingRepository _ratingRepository;

        private IOwnerRatingRepository _ownerRatingRepository;

        private IAccommodationReservationRepository _accommodationReservationRepository;

        public RatingService(IRatingRepository ratingRepository, IOwnerRatingRepository ownerRatingRepository, IAccommodationReservationRepository accommodationReservationRepository)
        {
            _ratingRepository = ratingRepository;
            _ownerRatingRepository = ownerRatingRepository;
            _accommodationReservationRepository = accommodationReservationRepository;
        }

        public List<Rating> FindAll()
        {
            return _ratingRepository.FindAll();
        }

        public List<Rating> FindAllSuitableByGuestId(int guestId)
        {
            List<Rating> temp = FindAll();
            List<Rating> results = new List<Rating>();
            foreach(Rating r in temp)
            {
                if (_accommodationReservationRepository.FindById(r.AccommodationReservationId).GuestId == guestId && _ownerRatingRepository.CheckReservationId(r.AccommodationReservationId))
                {
                    results.Add(r);
                }
            }

            return results;
        }
    }
}
