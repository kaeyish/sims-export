using BookingApp.Domain.Model;
using BookingApp.View;
using BookingApp.ViewModel;
using BookingApp.WPF.ViewModel;
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

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for AccommodationRecommendRenovationForm.xaml
    /// </summary>
    public partial class AccommodationRecommendRenovationForm : Window
    {
        int ReservationId;

        private User _loggedUser;

        private RecommendRenovationViewModel _viewModel;


        public AccommodationRecommendRenovationForm(User user, int reservationId)
        {   
            InitializeComponent();
            ReservationId = reservationId;
            _loggedUser = user;
            _viewModel = new RecommendRenovationViewModel(_loggedUser);
            DataContext = _viewModel;
        }

        private void CreateRecommendation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_viewModel.SaveRecommendation(ReservationId, int.Parse(Necessity.Text), Comment.Text));
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            GuestForm guestForm = new GuestForm(_loggedUser);
            guestForm.Show();
            Close();
        }
    }
}
