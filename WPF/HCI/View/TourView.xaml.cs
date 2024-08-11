using BookingApp.Domain.Model;
using BookingApp.ViewModel;
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
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : Page
    {
        private TourViewModel _tourViewModel;
        public TourView(int id, User user, Frame frame)
        {
            InitializeComponent();
            _tourViewModel = new TourViewModel(id, user, frame);
            DataContext = _tourViewModel;
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            _tourViewModel.SwitchToReservation();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _tourViewModel.Back();
        }
    }
}
