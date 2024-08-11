using BookingApp.Domain.Model;
using BookingApp.Service;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private TouristService _service;

        private string _name;

        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("LastName"); }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }

        private User _user;

        private Frame _frame;

        private Home _home;

        public ProfileViewModel(User user, Frame frame, Home home) {
            var app = (App)Application.Current;
            _service = app.TouristService;
            Tourist tourist = _service.FindOne(user.Id);
            Name = tourist.FirstName;
            LastName = tourist.LastName;
            Username = user.Username;
            _frame = frame;
            _home = home;
            _user = user;
        }  



        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        internal void LogOut()
        {
            _home.LogOut();
        }

        internal void VoucherSwitch()
        {
            Vouchers vouchers = new Vouchers(_user, _frame);
            _frame.Navigate(vouchers);
        }

        internal void ToursClick()
        {
            _home.NavigateToTours();   
        }

        internal void HelpClick()
        {
            Help help = new Help();
            _frame.Navigate(help);
        }
    }
}
