using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for OwnerForm.xaml
    /// </summary>
    public partial class OwnerForm : Window
    {
        public User LoggedInUser { get; set; }

        public AccommodationRepository AccommodationRepository { get; set; }

        public AccommodationReservationRepository AccommodationReservationRepository { get; set; }

        public UserRepository UserRepository { get; set; }

        public OwnerForm()
        {
            InitializeComponent();
        }

        public OwnerForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            AccommodationReservationRepository = new AccommodationReservationRepository();
            AccommodationRepository = new AccommodationRepository();
            UserRepository = new UserRepository();
            Username.Content = user.Username;
            Role.Content = user.Role;
        }

        private void RegisterAccommodationButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationForm registerAccommodationForm = new RegisterAccommodationForm(LoggedInUser);
            registerAccommodationForm.Show();
            Close();
        }

        private void RateGuestButton_Click(object sender, RoutedEventArgs e)
        {
            RateGuestForm rateGuestForm = new RateGuestForm(LoggedInUser);
            rateGuestForm.Show();
            Close();
        }

        private void ShowNotification_Click(object sender, RoutedEventArgs e)
        {
            List<AccommodationReservation> unratedGuests = FindReservationsByOwnerId(LoggedInUser.Id);
            List<int> toDisplay = new List<int>(); 

            foreach(var unratedGuest in unratedGuests)
            {
                toDisplay.Add(unratedGuest.Id);
            }


            listBox.ItemsSource = toDisplay;
            notificationPopup.IsOpen = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem != null)
            {
                RateGuestForm rateGuestForm = new RateGuestForm(LoggedInUser, listBox.SelectedItem.ToString());
                rateGuestForm.Show();
            }
            listBox.SelectedItem = null;
        }

        private List<AccommodationReservation> FindReservationsByOwnerId(int ownerId)
        {
            List<Accommodation> accommodations = AccommodationRepository.FindAll();
            List<AccommodationReservation> reservations = AccommodationReservationRepository.FindAll();
            List<User> guests = UserRepository.FindAll();

            var ownerAccommodationIds = accommodations.Where(a => a.OwnerId == ownerId).Select(a => a.Id).ToList();
            var filteredReservations = reservations.Where(r => (ownerAccommodationIds.Contains(r.AccommodationId)) & (r.EndDate.AddDays(5) >= DateTime.Now))
                                      .ToList();

            return filteredReservations;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerRatingsForm ownerRatingsForm = new OwnerRatingsForm(LoggedInUser);
            ownerRatingsForm.Show();
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            RenovationForm renovationForm = new RenovationForm(LoggedInUser);
            renovationForm.Show();
            Close();
        }
    }
}
