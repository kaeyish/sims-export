using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/ratings.csv";

        private readonly Serializer<Rating> _serializer;

        private List<Rating> _ratings;

        public RatingRepository()
        {
            _serializer = new Serializer<Rating>();
            _ratings = _serializer.FromCSV(FilePath);
        }

        public void RateGuest(Rating rating)
        {
            rating.Id = NextId();
            _ratings = _serializer.FromCSV(FilePath);
            _ratings.Add(rating);
            _serializer.ToCSV(FilePath, _ratings);
        }

        public int NextId()
        {
            _ratings = _serializer.FromCSV(FilePath);
            if (_ratings.Count < 1)
            {
                return 1;
            }
            return _ratings.Max(a => a.Id) + 1;
        }

        public List<Rating> FindAll()
        {
            return _serializer.FromCSV(FilePath);

        }


    }
}
