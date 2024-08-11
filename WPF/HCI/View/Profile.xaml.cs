using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private ProfileViewModel _viewModel;

        public Profile(User user, Frame frame, Home home)
        {
            InitializeComponent();
            _viewModel = new ProfileViewModel(user, frame, home);
            DataContext = _viewModel;
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LogOut();
        }

        private void Vouchers_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.VoucherSwitch();
        }
        
        private void NotImplemented(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented!");
        }



        private void BackToTours_Click(object sender, RoutedEventArgs e)
        {
             _viewModel.ToursClick();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.HelpClick();
        }
    }
}
