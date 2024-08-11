using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationReservationChangeRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservationChangeRequests.csv";

        private readonly Serializer<AccommodationReservationChangeRequest> _serializer;

        private List<AccommodationReservationChangeRequest> _reservations;

        public AccommodationReservationChangeRequestRepository()
        {
            _serializer = new Serializer<AccommodationReservationChangeRequest>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public List<AccommodationReservationChangeRequest> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
