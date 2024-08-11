using BookingApp.Domain.Model;
using BookingApp.Service;
using BookingApp.View;
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

namespace BookingApp.ViewModel
{
    public class TourViewModel : INotifyPropertyChanged
    {
        private Tour _tour { get; set; }

        public Tour Tour {
            get { return _tour; }
            set { _tour = value; OnPropertyChanged("Tour"); }
        }

        private TourService _service;

        private string _dates;

        public string Dates { 
            get { return _dates; }
            set { _dates = value; OnPropertyChanged("Dates"); }
        }

        private ObservableCollection<string> _picturePaths;
        public ObservableCollection<string> PicturePaths {
            get { return _picturePaths; }
            set { _picturePaths = value; OnPropertyChanged("PicturePaths"); }
        }

        private ObservableCollection<string> _checkpoints;
        public ObservableCollection<string> Checkpoints{
            get { return _checkpoints; }
            set { _checkpoints = value; OnPropertyChanged("Checkpoints"); }
        }



        private User _user;

        private Frame _frame;

        public TourViewModel(int tourId, User user, Frame frame) {
            var app = (App)Application.Current;
            _service = app.TourService;
            _user = user;
            _frame = frame;
            Tour = _service.FindById(tourId);
            Dates = _service.GetAllDates(Tour);
            PicturePaths = new ObservableCollection<string>(_service.ParseImagePaths(Tour));
            Checkpoints = new ObservableCollection<string>(_service.AllCheckpoints(Tour));
            
        }

        public void SwitchToReservation() {
            Reservation reservation = new Reservation(_user, _frame, Tour);
            _frame.Navigate(reservation);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        internal void Back()
        {
            MessageBox.Show("Not Implemented!");
        }
    }
}
