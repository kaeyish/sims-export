using BookingApp.Domain.Model;
using BookingApp.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        List<int> ExtractTourIds(int id);
        List<TourReservation> FindAll();
        List<TourReservation> FindAllByInstance(int id);
        List<TourReservation> FindAllByUser(int id);
        TourReservation FindOne(int id);
        int NextId();
        void Save(TourReservation reservation);

        TourReservation FindByTourAndUser(int tourId, int userId);

        List<Person> GetNotJoined(int tourId, int userId);
        List<Person> GetJoined(int tourId, int userId);
    }
}