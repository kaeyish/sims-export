using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.ViewModel;
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
    /// Interaction logic for MyReservationsView.xaml
    /// </summary>
    public partial class MyReservationsView : Window
    {
        private User _loggedUser;

        private MyReservationsViewModel _viewModel;

        public MyReservationsView(User user)
        {
            InitializeComponent();
            _loggedUser = user;
            _viewModel = new MyReservationsViewModel(_loggedUser);
            DataContext = _viewModel;
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_viewModel.CancelReservation(ReservationId.Text));
        }

        private void SubmitRating_Click(object sender, RoutedEventArgs e)
        {
            bool provera = _viewModel.SubmitRating(int.Parse(ReservationId.Text), int.Parse(CleanlinessRating.Text), int.Parse(OwnerBehavior.Text), AdditionalComment.Text, Photos.Text);
            if(!provera)
            {
                MessageBox.Show("Neuspesno dodat rejting!");
            }

            if (provera)
            {
                var result = MessageBox.Show("Uspesno dodat rejting!", "Da li zelis i da preporucis renoviranje?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    AccommodationRecommendRenovationForm accommodationRecommendRenovationForm = new AccommodationRecommendRenovationForm(_loggedUser, int.Parse(ReservationId.Text));
                    accommodationRecommendRenovationForm.Show();
                    Close();
                }
            }
        }

    }
}
