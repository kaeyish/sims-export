using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;

        }

        public List<Accommodation> FindAll() 
        {
            return _serializer.FromCSV(FilePath);

        }

        public List<Accommodation> FindByParams(SearchAccommodationDTO searchAccommodationDTO)
        {
            List<Accommodation> accommodations = new List<Accommodation>();

            _accommodations = _serializer.FromCSV(FilePath);
            foreach(Accommodation accommodation in _accommodations)
            {
                if(IsAMatch(searchAccommodationDTO, accommodation))
                {
                    accommodations.Add(accommodation);
                }
            }

            return accommodations;
        }

        public Accommodation FindById(int id)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            foreach (Accommodation accommodation in _accommodations)
            {
                if (accommodation.Id==id)
                {
                    return accommodation;
                }
            }

            return null;
        }

        public List<int> FindIdsByOwnerId(int ownerId)
        {
            List<int> ids = new List<int>();

            _accommodations = _serializer.FromCSV(FilePath);
            foreach (Accommodation accommodation in _accommodations)
            {
                if (accommodation.OwnerId == ownerId)
                {
                    ids.Add(accommodation.Id);
                }
            }

            return ids;
        }

        public bool IsAMatch(SearchAccommodationDTO searchAccommodationDTO, Accommodation accommodation) 
        {
            bool doesNameMatch = searchAccommodationDTO.Name.Equals(accommodation.Name) || searchAccommodationDTO.Name == "";
            bool doesLocationMatch = searchAccommodationDTO.Location.Equals(accommodation.Location) || searchAccommodationDTO.Location.Equals(new Location("", ""));
            bool doesTypeMatch = searchAccommodationDTO.AccommodationType == accommodation.AccommodationType || searchAccommodationDTO.AccommodationType == AccommodationType.None;
            bool doesGuestNumberMatch = searchAccommodationDTO.GuestNumber <= accommodation.MaxGuestNumber;
            bool doesReservationDaysMatch = searchAccommodationDTO.ReservationDays >= accommodation.MinReservationDays || searchAccommodationDTO.ReservationDays == 0;

            return doesNameMatch && doesLocationMatch && doesTypeMatch && doesGuestNumberMatch && doesReservationDaysMatch; 
        }




        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(a => a.Id) + 1;
        }


    }
}
