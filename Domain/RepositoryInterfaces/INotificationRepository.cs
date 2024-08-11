using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface INotificationRepository
    {
        List<Notification> FindAll(User user);
        List<Notification> FindAll(int userID);
        Notification FindOne(int id);
        void Save(Notification notification);
    }
}