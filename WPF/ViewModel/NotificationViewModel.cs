using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Service;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private NotificationService _service;

        public event PropertyChangedEventHandler? PropertyChanged;


        private ObservableCollection<Notification> _notifications { get; set; }

        public ObservableCollection<Notification> Notifications
        {
            get { return _notifications; }
            set { _notifications = value; OnPropertyChanged("Notifications"); }
        }

        private User _loggedUser;

        private Frame _frame;

        public NotificationViewModel(User user, Frame frame)
        {
            var app = (App)Application.Current;
            _service = app.NotificationService;
            _loggedUser = user;
            _frame = frame;
            Notifications = new ObservableCollection<Notification>(_service.FindAll(_loggedUser));
        }
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public void SwitchToTour(Notification notif)
        {
            if (notif.TourId.HasValue)
            {
                TourView tour = new TourView(notif.TourId.Value, _loggedUser, _frame);
                _frame.Navigate(tour);
            }


        }

        internal void Test()
        {
            if (_service.SendCheckIn(new Tour(), _loggedUser.Id))
            {
                MessageBox.Show("Osvojen Vaucer!");
            }
            else MessageBox.Show("Nije ispunjen uslov za osvajanje vaucera.");

            Notifications = new ObservableCollection<Notification>(_service.FindAll(_loggedUser));

        }
    }
}

