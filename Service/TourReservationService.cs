using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _repository;

        public TourReservationService(ITourReservationRepository tourReservationRepository) { 
            _repository = tourReservationRepository;
        }

        public void Save(TourReservation reservation)
        {
            _repository.Save(reservation);
        }

        public TourReservation FindOne(int id)
        {
            return _repository.FindOne(id);
        }

        public List<TourReservation> FindAll()
        {
            return _repository.FindAll();
        }

        public List<TourReservation> FindAllByUser(User user)
        {
            return _repository.FindAllByUser(user.Id);
        }

        public List<Person> GetJoined(int tourId, int userId)
        {
            return _repository.GetJoined(tourId, userId);
        }

        public List<Person> GetNotJoined(int tourId, int userId)
        {
           return _repository.GetNotJoined(tourId,userId);
        }

        public TourReservation FindByTourAndUser(int tourId, int userId) {
            return _repository.FindByTourAndUser(tourId, userId);
        }

        public List<TourReservation> FindAllByInstance(TourInstance instance)
        {
            return _repository.FindAllByInstance(instance.Id);
        }

       
    }
}
