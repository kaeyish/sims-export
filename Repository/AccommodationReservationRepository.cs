using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _reservations;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, _reservations);
            return accommodationReservation;

        }

        public List<AccommodationReservation> FindAll()
        {
            return _serializer.FromCSV(FilePath);

        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(a => a.Id) + 1;
        }

        public List<AccommodationReservation> FindAllByAccommodationId(int accommodationId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach(var reservation in _serializer.FromCSV(FilePath))
            {
                if(reservation.AccommodationId == accommodationId)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;

        }


        public AccommodationReservation FindById(int accommodationReservationId)
        {
            foreach (var reservation in _serializer.FromCSV(FilePath))
            {
                if (reservation.Id == accommodationReservationId)
                {
                    return reservation;
                }
            }

            return null;

        }

        public void Delete(int reservationId)
        {
            _reservations = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = _reservations.Find(c => c.Id == reservationId);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }


        public List<AccommodationReservation> FindAllByGuestId(int guestId)
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            foreach (var reservation in _serializer.FromCSV(FilePath))
            {
                if (reservation.GuestId == guestId)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;


        }
    }
}
