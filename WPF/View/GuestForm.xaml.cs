using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.DTO;
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
    /// Interaction logic for GuestForm.xaml
    /// </summary>
    public partial class GuestForm : Window
    {
        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _accommodationRepository;

        private readonly UserRepository _userRepository;

        public GuestForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _accommodationRepository = new AccommodationRepository();
            _userRepository = new UserRepository();
        }

        private void FindByParamsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAccommodationDTO searchAccommodationDTO = new SearchAccommodationDTO(Name.Text, new Location(City.Text, Country.Text), AccommodationType.Text.Equals("") ? 0 : (AccommodationType)int.Parse(AccommodationType.Text), GuestNumber.Text.Equals("") ? 0 : int.Parse(GuestNumber.Text), ReservationDays.Text.Equals("") ? 0 : int.Parse(ReservationDays.Text));

            List<Accommodation> accommodations = _accommodationRepository.FindByParams(searchAccommodationDTO);

            List<Accommodation> superOwnerPrioritized = new List<Accommodation>();

            foreach (Accommodation accommodation in accommodations)
            {
                if (_userRepository.GetById(accommodation.OwnerId).Role.Equals("Superowner"))
                {
                    superOwnerPrioritized.Insert(0, accommodation);
                }
                else
                {
                    superOwnerPrioritized.Add(accommodation);
                }
            }

            updateOutput(superOwnerPrioritized);
        }

        private void FindAllButton_Click(object sender, RoutedEventArgs e)
        {
            List<Accommodation> accommodations = _accommodationRepository.FindAll();
            
            List<Accommodation> superOwnerPrioritized = new List<Accommodation>();

            foreach(Accommodation accommodation in accommodations)
            {
                if (_userRepository.GetById(accommodation.OwnerId).Role.Equals("Superowner"))
                {
                    superOwnerPrioritized.Insert(0, accommodation);
                }
                else
                {
                    superOwnerPrioritized.Add(accommodation);
                }
            }

            updateOutput(superOwnerPrioritized);
        }

        private void MyReservations_Click(object sender, RoutedEventArgs e)
        {
            MyReservationsView myReservationsView= new MyReservationsView(LoggedInUser);
            myReservationsView.Show();
            Close();
        }

        private void updateOutput(List<Accommodation> accommodations)
        {
            Output.Text = "";
            foreach (Accommodation accommodation in accommodations)
            {
                Output.Text += accommodation.ToString() + "\n";
            }
        }

        private void CreateReservatinoForm_Click(object sender, RoutedEventArgs e)
        {

            RegisterAccommodationReservationForm accommodationReservationForm = new RegisterAccommodationReservationForm(LoggedInUser, AccommodationId.Text.Equals("") ? 0 : int.Parse(AccommodationId.Text));
            accommodationReservationForm.Show();
            Close();
        }

        private void MyRatings_Click(object sender, RoutedEventArgs e)
        {
            GuestRatingsForm guestRatingsForm = new GuestRatingsForm(LoggedInUser);
            guestRatingsForm.Show();
            Close();
        }
    }
}
