using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Xps.Serialization;

namespace BookingApp.Repository
{
    public class NotificationRepository : INotificationRepository
    {

        private const string FilePath = "../../../Resources/Data/notifications.csv";

        private readonly Serializer<Notification> _serializer;

        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _serializer = new Serializer<Notification>();
            _notifications = _serializer.FromCSV(FilePath);
        }

        public void Save(Notification notification)
        {
            notification.Id = NextId();
            _notifications = _serializer.FromCSV(FilePath);
            _notifications.Add(notification);
            _serializer.ToCSV(FilePath, _notifications);
        }

        public List<Notification> FindAll(User user)
        {
            List<Notification> result = _notifications.Where(u => u.UserId == user.Id).ToList();
            return result;
        }

        public List<Notification> FindAll(int id)
        {
            List<Notification> result = _notifications.Where(u => u.UserId == id).ToList();
            return result;
        }

        public Notification FindOne(int id)
        {
            return _notifications.FirstOrDefault(u => u.Id == id);
        }


        public int NextId()
        {
            _notifications = _serializer.FromCSV(FilePath);
            if (_notifications.Count < 1)
            {
                return 1;
            }
            return _notifications.Max(a => a.Id) + 1;
        }
    }
}
