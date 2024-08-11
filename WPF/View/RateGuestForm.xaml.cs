using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for RateGuestForm.xaml
    /// </summary>
    public partial class RateGuestForm : Window
    {
        public User LoggedInUser { get; set; }

        public AccommodationReservation AccommodationReservation { get; set; }

        private readonly RatingRepository _repository;

        private readonly AccommodationReservationRepository _reservationRepository;

        private readonly UserRepository _userRepository;

        public RateGuestForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new RatingRepository();
            _reservationRepository = new AccommodationReservationRepository();
            _userRepository = new UserRepository();
        }

        public RateGuestForm(User user, string id)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new RatingRepository();
            _userRepository = new UserRepository();
            _reservationRepository = new AccommodationReservationRepository();
            AccommodationReservation = _reservationRepository.FindById(int.Parse(id));
            GuestName.Text = _userRepository.GetById(AccommodationReservation.GuestId).Username;
        }

        private void RateGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Rating newRating = new Rating(GuestName.Text, LoggedInUser.Username, AccommodationReservation.Id, int.Parse(Cleanliness.Text), int.Parse(RuleFollowing.Text), Comment.Text);
            try
            {
                _repository.RateGuest(newRating);
                MessageBox.Show("Uspesno dodato!!!");
            }
            catch (Exception ex)
            {
                // FormatException ako se za tipa maxGuestNumber unese string...
                MessageBox.Show("Ohh-ooooohhh greskica pri unosu !!!\n" + ex.ToString());
            }
        }
    }
}
