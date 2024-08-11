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
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : Page
    {
        private SimpleRequestViewModel _viewModel;
        public Requests(User user, Frame frame)
        {
            InitializeComponent();
            _viewModel = new SimpleRequestViewModel(user, frame);
            DataContext = _viewModel;

        }


        private void Pending_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.RefreshSimple(State.Pending);
        }

        private void Rejected_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.RefreshSimple(State.Invalid);
        }

        private void Approved_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.RefreshSimple(State.Accepted);
        }

        private void All_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.RefreshSimple(null);
        }


        private void ApprovedComplex_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshComplex(State.Accepted);
        }

        private void RejectedComplex_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.RefreshComplex(State.Invalid);
        }

        private void PendingComplex_Click(object sender, RoutedEventArgs e)
        {

            _viewModel.RefreshComplex(State.Pending);
        }

        private void AllComplex_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshComplex(null);
        }
    }
}
