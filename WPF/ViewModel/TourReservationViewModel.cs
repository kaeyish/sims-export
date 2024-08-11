using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
using BookingApp.View;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.ViewModel
{
    public class TourReservationViewModel : INotifyPropertyChanged
    {
        private int counter = 1;

        private TourInstance _instance;

        public int AvailableCapacity { 
            get { return _instance.AvailableCapacity; }
            set { _instance.AvailableCapacity = value; OnPropertyChanged("AvailableCapacity"); }
        }

        private Location _location;

         private TourReservationService _service;

        private TourService _tourService;

        private TourReservation _reservation;

        private List<Person> _people;

        public List<Person> People {
            get { return _people;  }
            set { _people = value; OnPropertyChanged("People"); }
        }

        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _tourname;

        public string TourName
        {
            get { return _tourname; }
            set { _tourname = value; OnPropertyChanged("TourName"); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("LastName"); }
        }
        private string _age;

        public string Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        private string _partySize { get; set; }
        public string PartySize {
            get { return _partySize; }
            set { _partySize = value; OnPropertyChanged("PartySize"); }
        }

        private ObservableCollection<DateTime> _dates;

        public ObservableCollection<DateTime> Dates {
            get { return _dates; }
            set { _dates = value; OnPropertyChanged("Dates"); }
        }


        private Tour _tour;

        private User _user;

        private Frame _frame;

        public TourReservationViewModel(User user, Tour tour, Frame frame)
        {
            var app = (App)Application.Current;
           _tourService = app.TourService;
            _service = app.TourReservationService;
            _reservation = new TourReservation();
            TourName = tour.Name;
            People = new List<Person>();
            Dates = new ObservableCollection<DateTime>(_tourService.ExtractDates(tour));
            _tour = tour;
            _user = user;
            _frame = frame;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        internal void InsertRedirect(String partySizeParam, User user)
        {
               
            }

        internal void VoucherRedirect(User user)
        {
           
        }

        internal void Confirm()
        {
            _reservation.People = People;
            _service.Save(_reservation);
            MessageBox.Show("Rezervacija zavrsena!");
            TourView tourView = new TourView(_instance.ParentId, _user, _frame);
            _frame.Navigate(tourView);
        }

        internal void AddPerson(string name, string lastName, string Age)
        {
            Person person = new Person();
            person.FirstName = name;
            person.LastName = lastName;
            person.Age = int.Parse(Age);
            People.Add(person);
        }

        internal void SelectedDate(string date)
        {
            _instance = _tourService.FindInstanceByDate(_tour, DateTime.Parse(date));
            _location = _tour.Location;
            _reservation.InstanceId = _instance.Id;
            _reservation.TouristId = _user.Id;
        }



        internal void ClearPeople()
        {
            _people.Clear();
        }

        internal void Back()
        {
            TourView tourView = new TourView(_tour.Id, _user, _frame);
            _frame.Navigate(tourView);
        }
    }
}
