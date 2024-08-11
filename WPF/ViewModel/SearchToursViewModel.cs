using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{
    public class SearchToursViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Domain.Model.Tour> _tours;

        public ObservableCollection<Domain.Model.Tour> Tours
        {
            get { return _tours; }

            set { _tours = value; OnPropertyChanged("Tours"); }

        }


        private TourService _service;

        private User _loggedUser;

        private string _noResult;

        public string NoResult
        {
            get { return _noResult; }
            set { _noResult = value; OnPropertyChanged("NoResult"); }
        }

        private Frame _activeFrame;


        private Tour _tour;
        public Tour SelectedTour
        {
            get { return _tour; }
            set { _tour = value; OnPropertyChanged("SelectedTour"); }
        }

        public SearchToursViewModel(Frame frame, User user)
        {
            _loggedUser = user;
            _activeFrame = frame;
            var app = (App)Application.Current;
            _service = app.TourService;
            Tours = new ObservableCollection<Tour>();
            NoResult = String.Empty;
        }
 

        public void Search(string city, string country, string language, string duration, string partySize)
        {
            try
            {
                Tours = new ObservableCollection<Tour>(_service.SearchTours(new Location(city, country), language, duration, partySize));
                if (!Tours.Any()) { NoResult = "No tours matched search criteria."; }
                else NoResult = string.Empty;
                SearchResults result = new SearchResults(this);
                _activeFrame.Navigate(result);
            }
            catch (Exception ex) { MessageBox.Show("Incorrect format for duration/party size."); }
        }

        public void ListAll()
        {
            Tours = new ObservableCollection<Tour>(_service.FindAll());
            SearchResults result = new SearchResults(this);
            _activeFrame.Navigate(result);
        }

       

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        internal void BackToSearch()
        {
            SearchParams searchParams = new SearchParams(_loggedUser, _activeFrame);
            _activeFrame.Navigate(searchParams);
        }

        internal void SwitchToTour(Tour? tour)
        {
            TourView tourView = new TourView(tour.Id, _loggedUser, _activeFrame);
            _activeFrame.Navigate(tourView);
        }
    }
}
