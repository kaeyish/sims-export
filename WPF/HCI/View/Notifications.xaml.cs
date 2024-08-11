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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.HCI.View
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Page
    {
        private NotificationViewModel _viewModel;
        public Notifications(User user, Frame frame)
        {
            InitializeComponent();
            _viewModel = new NotificationViewModel(user, frame);
            DataContext = _viewModel;
        }

        private void Tours_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Notification notif= button.DataContext as Notification;
            _viewModel.SwitchToTour(notif);
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Test();
        }
    }
}
