using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class TourRatingRepository : ITourRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRatings.csv";

        private readonly Serializer<TourRating> _serializer;

        private List<TourRating> _ratings;
    
        public TourRatingRepository ()
        {
            _serializer = new Serializer<TourRating>();
            _ratings = _serializer.FromCSV(FilePath);
        }

        public void Save(TourRating rating)
        {
            _ratings = _serializer.FromCSV(FilePath);
            rating.Id = NextId();
            _ratings.Add(rating);
            _serializer.ToCSV(FilePath, _ratings);
        }

        public List<TourRating> FindAll() {
            return _ratings;
        }

        public List<TourRating> FindAllByUser(int id) {
            return _ratings.FindAll(x => x.TouristId == id);
        }

        public List<TourRating> FindAllByTour(int id)
        {
            return _ratings.FindAll(x => x.TourId == id);
        }

        public List<TourRating> FindAllByTourAndUSer(int tourId, int userId)
        {
            return _ratings.FindAll(x => x.TourId == tourId && x.TouristId == userId);
        }



        public TourRating FindOne(int id) {
            return _ratings.Find(c => c.Id == id);
        }

     
        public int NextId()
        {
            if (_ratings.Count < 1)
            {
                return 1;
            }
            return _ratings.Max(c => c.Id) + 1;
        }


    }
}
