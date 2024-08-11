using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace BookingApp.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservations.csv";

        private readonly Serializer<TourReservation> _serializer;

        private List<TourReservation> _reservations;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _reservations = _serializer.FromCSV(FilePath);
        }

        public void Save(TourReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            reservation.Id = NextId();
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public List<TourReservation> FindAll()
        {
            return _reservations;
        }

        public List<int> ExtractTourIds(int id)
        {
            List<int> ids = new List<int>();

            List<TourReservation> reservations = FindAllByUser(id);

            foreach (var reservation in reservations) { ids.Add(reservation.InstanceId); }

            return ids;

        }

        public List<Person> GetJoined(int tourId, int userId) {
            TourReservation reservation = FindByTourAndUser(tourId, userId);

            List<Person> joined = new List<Person>();

            foreach (var person in reservation.People) {
                if (person.Check == Check.CheckedIn)
                    joined.Add(person);
            }

            return joined;
        }

        public List<Person> GetNotJoined(int tourId, int userId)
        {
            TourReservation reservation = FindByTourAndUser(tourId, userId);

            return reservation.People.FindAll(x => x.Check == Check.Pending);
        }

        public TourReservation FindByTourAndUser(int tourId, int userId)
        {
            return _reservations.First(x => x.InstanceId == tourId && x.TouristId == userId);
        }

        public List<TourReservation> FindAllByUser(int id)
        {
            return _reservations.FindAll(x => x.TouristId == id);
        }

        public List<TourReservation> FindAllByInstance(int id)
        {
            return _reservations.FindAll(x => x.InstanceId == id);
        }

        public TourReservation FindOne(int id)
        {
            return _reservations.Find(c => c.Id == id);
        }

        public int NextId()
        {
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(c => c.Id) + 1;
        }


    }
}
