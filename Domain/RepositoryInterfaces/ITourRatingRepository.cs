using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRatingRepository
    {
        public void Save(TourRating rating);
        public List<TourRating> FindAll();

        public List<TourRating> FindAllByUser(int id);

        public List<TourRating> FindAllByTour(int id);

        public TourRating FindOne(int id);
    }
}
