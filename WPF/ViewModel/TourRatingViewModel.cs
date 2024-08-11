using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using BookingApp.WPF.HCI.View;
using System.Collections.ObjectModel;

namespace BookingApp.WPF.ViewModel
{
    public class TourRatingViewModel : INotifyPropertyChanged
    {
        private TourRatingService _service;

        private TourRating _rating;

        private string _knowledge;
        public string Knowledge
        {
            get { return _knowledge; }
            set { _knowledge = value; OnPropertyChanged("Knowledge"); }
        }

        private string _fluency;
        public string Fluency
        {
            get { return _fluency; }
            set { _fluency = value; OnPropertyChanged("Fluency"); }
        }

        private string _amusement;
        public string Amusement
        {
            get { return _amusement; }
            set { _amusement = value; OnPropertyChanged("Amusement"); }
        }

        private string _pictures;

        public string Pictures
        {
            get { return _pictures; }
            set { _pictures = value; OnPropertyChanged("Pictures"); }
        }

        public TourRating TourRating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged("Rating"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private InstancePackDto _instance;
        private User _loggedUser;

        private Frame _frame;

        private string _tourName;
        
        public string TourName {
            get { return _tourName; }
            set { _tourName = value; OnPropertyChanged("TourName"); }
        }

        public TourRatingViewModel(User user, InstancePackDto data, Frame frame)
        {
            _loggedUser = user;
            var app = (App)System.Windows.Application.Current;
            _service = app.TourRatingService;
            _instance = data;
            TourRating = new TourRating();
            TourRating.TourId = _instance.TourInstance.ParentId;
            TourRating.TouristId = user.Id;
            TourName = data.Name;
            _loggedUser = user;
            _frame = frame;
        }

        public void Save()
        {
            try
            {
                TourRating.GuideKnowledge = int.Parse(Knowledge);
                TourRating.GuideLanguageFLuency = int.Parse(Fluency);
                TourRating.Amusement = int.Parse(Amusement);
                TourRating.Pictures.AddRange(Pictures.Split(","));
                _service.Save(TourRating);
                Tours tours = new Tours(_loggedUser, _frame);
                _frame.Navigate(tours);

            }
            catch (Exception ex) { MessageBox.Show("Parsing error."); }

        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

    }
}
