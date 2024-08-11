using BookingApp.Domain.Model;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.HCI.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window, INotifyPropertyChanged
    {
        User _loggedUser;

        private bool _isDemoOn;

        public bool IsDemoOn {
            get { return _isDemoOn; } set { _isDemoOn = value; OnPropertyChanged("IsDemoOn"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public Home(User user)
        {
            InitializeComponent();
            _loggedUser = user;
            IsDemoOn = false;
            DataContext = this;
            NavigateToTours();
        }

        public void NavigateToTours()
        {
            ToursHeader.Visibility = Visibility.Visible;
            HomeButton.IsDefault = true;
            Search.IsDefault = false;
            Notifications.IsDefault = false;
            Profile.IsDefault = false;
            Help.IsDefault = false;
            ToursHeader.Visibility = Visibility.Visible;
            Reviews.IsDefault = false;
            Tours.IsDefault = true;
            Requests.IsDefault = false;
            Tours tours = new Tours(_loggedUser, MainFrame);
            MainFrame.Navigate(tours);
        }

        private void Reviews_Click(object sender, RoutedEventArgs e)
        {
            Reviews.IsDefault = true;
            Tours.IsDefault = false;
            Requests.IsDefault = false;
            Reviews reviews = new Reviews(_loggedUser);
            MainFrame.Navigate(reviews); 
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            Reviews.IsDefault = false;
            Tours.IsDefault = false;
            Requests.IsDefault = true;
            Requests requests = new Requests(_loggedUser, MainFrame);
            MainFrame.Navigate(requests); 
            
        }

        private void Tours_Click(object sender, RoutedEventArgs e)
        {
            ToursHeader.Visibility = Visibility.Visible;
            NavigateToTours();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            
            NavigateToTours();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            ToursHeader.Visibility = Visibility.Collapsed;
            HomeButton.IsDefault = false;
            Search.IsDefault = true;
            Notifications.IsDefault = false;
            Profile.IsDefault = false;
            Help.IsDefault = false;
            SearchParams search = new SearchParams(_loggedUser, MainFrame);
            MainFrame.Navigate(search);
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            ToursHeader.Visibility = Visibility.Collapsed;
            HomeButton.IsDefault = false;
            Search.IsDefault = false;
            Notifications.IsDefault = true;
            Profile.IsDefault = false;
            Help.IsDefault = false;
            Notifications notifs = new Notifications(_loggedUser, MainFrame);
            MainFrame.Navigate(notifs);
        }

        public void LogOut() {
            SignInForm signInForm = new SignInForm();
            signInForm.Show();

            this.Close();
        }

        public Home Get()
        {
            return this;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {

            ToursHeader.Visibility = Visibility.Collapsed;
            HomeButton.IsDefault = false;
            Search.IsDefault = false;
            Notifications.IsDefault = false;
            Profile.IsDefault = true;
            Help.IsDefault = false;
            Profile profile = new Profile(_loggedUser, MainFrame, this);
            MainFrame.Navigate(profile);
        }

        public  void Help_Click(object sender, RoutedEventArgs e)
        {

            ToursHeader.Visibility = Visibility.Collapsed;
            HomeButton.IsDefault = false;
            Search.IsDefault = false;
            Notifications.IsDefault = false;
            Profile.IsDefault = false;
            Help.IsDefault = true;
            MainFrame.Navigate(new Help());
        }
    }
}
