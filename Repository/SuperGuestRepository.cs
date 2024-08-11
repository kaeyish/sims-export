using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class SuperGuestRepository : ISuperGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/SuperGuest.csv";

        private readonly Serializer<SuperGuest> _serializer;

        private List<SuperGuest> _superGuests;

        public SuperGuestRepository()
        {
            _serializer = new Serializer<SuperGuest>();
            _superGuests = _serializer.FromCSV(FilePath);
        }

        public SuperGuest Save(SuperGuest superGuest)
        {
            superGuest.Id = NextId();
            _superGuests = _serializer.FromCSV(FilePath);
            _superGuests.Add(superGuest);
            _serializer.ToCSV(FilePath, _superGuests);
            return superGuest;

        }

        public List<SuperGuest> FindAll()
        {
            return _serializer.FromCSV(FilePath);

        }

        public SuperGuest FindByGuestId(int guestId)
        {
            foreach (var guest in _serializer.FromCSV(FilePath))
            {
                if (guest.GuestId == guestId)
                {
                    return guest;
                }
           }
            return null;
        }


        public void DeleteByGuestId(int guestId)
        {
            _superGuests = _serializer.FromCSV(FilePath);
            SuperGuest founded = _superGuests.Find(c => c.GuestId == guestId);
            _superGuests.Remove(founded);
            _serializer.ToCSV(FilePath, _superGuests);
        }

        public int NextId()
            {
                _superGuests = _serializer.FromCSV(FilePath);
                if (_superGuests.Count < 1)
                {
                    return 1;
                }
                return _superGuests.Max(a => a.Id) + 1;
            }

        public void Update(SuperGuest superGuest)
        {
            _superGuests = _serializer.FromCSV(FilePath);
            List<SuperGuest> tempGuests = new List<SuperGuest>();
            foreach (SuperGuest super in _superGuests)
            {
                if (super.Id == superGuest.Id)
                {
                    tempGuests.Add(superGuest);
                }
                else tempGuests.Add(super);
            }
            _superGuests = tempGuests;
            _serializer.ToCSV(FilePath, _superGuests);
        }
    }
}
