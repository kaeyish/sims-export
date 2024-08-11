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
    /// Interaction logic for SearchResults.xaml
    /// </summary>
    public partial class SearchResults : Page
    {
        private SearchToursViewModel _viewModel;
        public SearchResults(SearchToursViewModel searchToursViewModel)
        {
            InitializeComponent();
            _viewModel = searchToursViewModel;
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.BackToSearch();
        }

        private void TourName_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = sender as Hyperlink;
            Tour tour = hyperlink.DataContext as Tour;
            _viewModel.SwitchToTour(tour);
        }
    }
}
