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
    public class RenovationRepository : IRenovationRepository
    {


        private const string FilePath = "../../../Resources/Data/renovations.csv";

        private readonly Serializer<Renovation> _serializer;

        private List<Renovation> _renovations;

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }

        public Renovation Save(Renovation renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;

        }

        public List<Renovation> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Renovation FindById(int id)
        {
            foreach (Renovation renovation in _serializer.FromCSV(FilePath))
            {
                if (renovation.Id == id)
                {
                    return renovation;
                }
            }

            return null;
        }

        public void DeleteById(int id)
        {
            _serializer.ToCSV(FilePath, _serializer.FromCSV(FilePath).Where(x => x.Id != id).ToList());
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(a => a.Id) + 1;
        }
    }
}
