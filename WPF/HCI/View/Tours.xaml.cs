using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : Page
    {
        private User _loggedUser;

        private MyToursViewModel _viewModel;

        public Tours(User user, Frame frame)
        {
            InitializeComponent();
            _loggedUser = user;
            _viewModel = new MyToursViewModel(user, null, frame);
            DataContext = _viewModel;
            All.IsChecked = true;
        }

        private void SwitchToRating(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            InstancePackDto dto = button.DataContext as InstancePackDto;
            _viewModel.SwitchToRating(dto);
        }

        private void SwitchToTracking(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            InstancePackDto dto = button.DataContext as InstancePackDto;
            _viewModel.SwitchToTracking(dto);
        }

        private void Past_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Refresh(Status.Finished);
        }

        private void OnGoing_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Refresh(Status.OnGoing);
        }

        private void Future_Click(object sender, RoutedEventArgs e)
        {
            
            _viewModel.Refresh(Status.NotStarted);
        }

        private void All_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Refresh();
        }

        public void CreatePdf(object sender, RoutedEventArgs e)
        {
            _viewModel.CreatePdf();
        }


    }
}
