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
    /// Interaction logic for SearchParams.xaml
    /// </summary>
    public partial class SearchParams : Page
    {

        SearchToursViewModel _viewModel;
        public SearchParams(User user, Frame MainFrame)
        {
            InitializeComponent();
            _viewModel = new SearchToursViewModel(MainFrame, user);
            DataContext = _viewModel;
        }

        public void SendParams(object sender, RoutedEventArgs e)
        {
            _viewModel.Search(CityParam.Text, CountryParam.Text, LanguageParam.Text, DurationParam.Text, PartySizeParam.Text);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void FindAll(object sender, RoutedEventArgs e)
        {
            _viewModel.ListAll();
        }

    }
}
