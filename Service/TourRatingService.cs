using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourRatingService
    {
        private ITourRatingRepository _repository;

        public TourRatingService(ITourRatingRepository repository) {
            _repository = repository;
        }

        public void Save(TourRating rating) {
            TourRepository repo = new TourRepository();
            Tour tour = repo.FindById(rating.TourId);
            rating.OverallRating = rating.CalculateOverall();
            tour.Rating += rating.OverallRating;
            tour.Rating /= 2;
            _repository.Save(rating);
            repo.Update(tour);
        }

        public TourRating FindOne(int id)
        {
            return _repository.FindOne(id);
        }

        public List<TourRating> FindAll ()
        {
            return _repository.FindAll();
        }

        public List<TourRating> FindAllByUser(User user)
        {
            return _repository.FindAllByUser(user.Id);
        }

        public List<TourRating> FindAllByTour(Tour tour)
        {
            return _repository.FindAllByTour(tour.Id);
        }



    }
}
