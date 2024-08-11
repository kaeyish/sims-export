using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel
{
    public class TourTrackingViewModel : INotifyPropertyChanged
    {
        private NotificationService _service;

        private TourService _tourService;

        private TourReservationService _reservationService;

        private User _loggedUser;

        private InstancePackDto _tour;

        public InstancePackDto Tour {
            get { return _tour; }
            set { _tour = value; OnPropertyChanged("Tour"); }
        }

        private Checkpoint _currentCheckpoint;

        public Checkpoint CurrentCheckpoint {
            get { return _currentCheckpoint; }
            set { _currentCheckpoint = value; OnPropertyChanged("CurrentCheckpoint"); }
        }

        private string _previousCheckpoints;

        public string PreviousCheckpoints
        {
            get { return _previousCheckpoints; }
            set { _previousCheckpoints = value; OnPropertyChanged("PreviousCheckpoints"); }
        }


        private string _upcomingCheckpoints;

        public string UpcomingCheckpoints
        {
            get { return _upcomingCheckpoints; }
            set { _upcomingCheckpoints = value; OnPropertyChanged("UpcomingCheckpoints"); }
        }

        private string _peopleJoined;

        public string PeopleJoined
        {
            get { return _peopleJoined; }
            set { _peopleJoined = value; OnPropertyChanged("PeopleJoined"); }
        }


        private string _peopleNotJoined;

        public string PeopleNotJoined
        {
            get { return _peopleNotJoined; }
            set { _peopleNotJoined = value; OnPropertyChanged("PeopleNotJoined"); }
        }

        private ObservableCollection<Notification> _notifications { get; set; }

        public ObservableCollection<Notification> Notifications
        {
            get { return _notifications; }
            set { _notifications = value; OnPropertyChanged("Notifications"); }
        }




        public TourTrackingViewModel(User user, InstancePackDto tour, TourService tourService) {
            _loggedUser = user;
            var app = (App)Application.Current;
            _service = app.NotificationService;
            _reservationService = app.TourReservationService;
            _tourService = tourService;
            Notifications = new ObservableCollection<Notification>(_service.FindAll(_loggedUser));
            Tour = tour;
            CurrentCheckpoint = _tourService.GetCurrent(tour.TourInstance.ParentId);
            PreviousCheckpoints = string.Join(',', _tourService.GetPrevoius(tour.TourInstance.ParentId));
            UpcomingCheckpoints = string.Join(',',_tourService.GetUpcoming(tour.TourInstance.ParentId));
            PeopleJoined = string.Join('/', _reservationService.GetJoined(tour.TourInstance.Id, user.Id));
            PeopleNotJoined = string.Join('/', _reservationService.GetNotJoined(tour.TourInstance.Id, user.Id));

        }


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }
    }
}
