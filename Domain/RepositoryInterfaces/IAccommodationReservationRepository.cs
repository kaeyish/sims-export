using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        public AccommodationReservation Save(AccommodationReservation accommodationReservation);

        public List<AccommodationReservation> FindAll();

        public int NextId();

        public List<AccommodationReservation> FindAllByAccommodationId(int accommodationId);
        
        public AccommodationReservation FindById(int accommodationReservationId);

        public void Delete(int reservationId);


        public List<AccommodationReservation> FindAllByGuestId(int guestId);
    }
}
