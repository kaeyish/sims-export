using BookingApp.Domain.Model;
using BookingApp.Model;
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
    /// Interaction logic for GuestRatingsForm.xaml
    /// </summary>
    public partial class GuestRatingsForm : Window
    {
        public User _loggedInUser;

        public GuestMyRatingsViewModel _viewModel;

        public GuestRatingsForm(User user)
        {
            InitializeComponent();
            _loggedInUser = user;
            _viewModel = new GuestMyRatingsViewModel();
            updateOutput(_viewModel.GetRatings(user.Id));
            DataContext = this;
        }

        private void updateOutput(List<Rating> ratings)
        {
            Output.Text = "";
            foreach (Rating rating in ratings)
            {
                Output.Text += rating.ToString() + "\n";
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            updateOutput(_viewModel.GetRatings(_loggedInUser.Id));
        }
    }
}
