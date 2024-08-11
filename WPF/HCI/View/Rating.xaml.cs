using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.HCI.View
{
    /// <summary>
    /// Interaction logic for Rating.xaml
    /// </summary>
    public partial class Rating : Page
    {
        private User _loggedUser;

        private TourRatingViewModel _viewModel;

        public Rating(User user, InstancePackDto data, Frame frame)
        {
            InitializeComponent();
            _loggedUser = user;
            _viewModel = new TourRatingViewModel(_loggedUser, data, frame);
            DataContext = _viewModel;
        }

            private void Save(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
        }
    }
}
