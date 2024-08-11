using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class AccommodationReservationService
    {

        private readonly AccommodationReservationRepository _repository = new();

        public AccommodationReservationService() { }

        public List<AccommodationReservationDto> FindAllByGuestId(int guestId)
        {
            List<AccommodationReservation> reservations = _repository.FindAllByGuestId(guestId);

            List<AccommodationReservationDto> reservationsDto = new List<AccommodationReservationDto>();

            foreach (var reservation in reservations)
            {
                reservationsDto.Add(new AccommodationReservationDto(reservation));
            }

            return reservationsDto;
        }

        public AccommodationReservation FindById(int reservationId)
        {
            return _repository.FindById(reservationId);
        }

        public void DeleteById(int reservationId)
        {
            _repository.Delete(reservationId);
        }

        public int CountByGuestIdLastYear(int guestId)
        {
            List<AccommodationReservation> reservations = _repository.FindAllByGuestId(guestId);
            int count = 0;

            foreach (var reservation in reservations)
            {
                if (reservation.StartDate > DateTime.Now.AddYears(-1))
                {
                    count++;
                }
            }

            return count;
        }

        public bool CheckConditionGuest(int guestId)
        {
            if(CountByGuestIdLastYear(guestId)<10)
            {
                return false;
            }
            return true;
        }

    }
}
