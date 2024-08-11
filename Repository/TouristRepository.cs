using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TouristRepository : ITouristRepository
    {
        private const string FilePath = "../../../Resources/Data/tourists.csv";

        private readonly Serializer<Tourist> _serializer;

        private List<Tourist> _tourists;

        public TouristRepository()
        {
            _serializer = new Serializer<Tourist>();
            _tourists = _serializer.FromCSV(FilePath);
        }

        public List<Tourist> FindAll() {
            return _tourists;
        }

        public Tourist FindOne(int id) {
            
            return _tourists.FirstOrDefault(u => u.Id == id);

        }

        public void Save(Tourist tourist)
        {
            _tourists.Add(tourist);
            _serializer.ToCSV(FilePath, _tourists);
        }

        public void Update(Tourist tourist) {
            List<Tourist> tempTours = new List<Tourist>();
            foreach (Tourist t in _tourists)
            {
                if (t.Id == tourist.Id)
                {
                    tempTours.Add(tourist);
                }
                else tempTours.Add(t);
            }
            _tourists = tempTours;
            _serializer.ToCSV(FilePath, _tourists);

        }


    }
}
