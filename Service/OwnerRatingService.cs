using Accessibility;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class OwnerRatingService
    {
        private OwnerRatingRepository _ownerRatingRepository;

        private UserRepository _userRepository;

        private AccommodationRepository _accommodationRepository;

        private AccommodationReservationRepository _accommodationReservationRepository;

        public OwnerRatingService()
        {
            _ownerRatingRepository = new OwnerRatingRepository();
            _userRepository = new UserRepository();
            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
        }

        public List<OwnerRatingDto> FindAllByOwnerId(int ownerId)
        {
            List<OwnerRatingDto> ownerRatingDtos = new List<OwnerRatingDto>();

            List<OwnerRating> ownerRatings = _ownerRatingRepository.FindAllByOwnerId(ownerId);

            foreach(var ownerRating in ownerRatings)
            {
                OwnerRatingDto ownerRatingDto = new OwnerRatingDto();

                ownerRatingDto.ownerUsername = _userRepository.GetById(ownerId).Username;
                ownerRatingDto.guestUsername = _userRepository.GetById(ownerRating.GuestId).Username;
                ownerRatingDto.accommodationName = _accommodationRepository.FindById(ownerRating.AccommodationId).Name;
                ownerRatingDto.accommodationReservation = _accommodationReservationRepository.FindById(ownerRating.AccommodationReservationId);
                ownerRatingDto.Cleanliness = ownerRating.Cleanliness;
                ownerRatingDto.OwnerCorectness = ownerRating.OwnerCorectness;
                ownerRatingDto.Comment = ownerRating.Comment;
                ownerRatingDto.Photos = ownerRating.Photos;

                ownerRatingDtos.Add(ownerRatingDto);
            }

            return ownerRatingDtos;


        }


        public void Save(OwnerRating ownerRating)
        {
            _ownerRatingRepository.Save(ownerRating);
            
            if(eligibleToBecomeSuperOwner(ownerRating.OwnerId))
            {
                _userRepository.PromoteToSuperOwner(ownerRating.OwnerId);
            }

            if (!eligibleToBecomeSuperOwner(ownerRating.OwnerId) && _userRepository.GetById(ownerRating.OwnerId).Role.Equals("Superowner"))
            {
                _userRepository.DemoteFromSuperOwner(ownerRating.OwnerId);
            }

        }

        public double CalculateAverage(int ownerId)
        {
            double sum = 0;
            int count = 0;

            List<OwnerRating> ownerRatings = _ownerRatingRepository.FindAllByOwnerId(ownerId);

            if (ownerRatings.Count() == 0)
            {
                return 0;
            }

            foreach (OwnerRating ownerRating in ownerRatings)
            {
                sum += (ownerRating.Cleanliness + ownerRating.OwnerCorectness) / 2;
                count++;
            }


            return (double)(sum / count);
        }

        public bool eligibleToBecomeSuperOwner(int ownerId)
        {
            return CalculateAverage(ownerId) >= 4.5 && _ownerRatingRepository.Count(ownerId) >= 50;
        }

        public bool IsAlreadyRated(int accommodationReservationId)
        {
            return _ownerRatingRepository.CheckReservationId(accommodationReservationId);
        }
    }
}
