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
    /// Interaction logic for RequestStats.xaml
    /// </summary>
    public partial class RequestStats : Page
    {
        SimpleRequestViewModel _viewModel;

        public RequestStats(SimpleRequestViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        
        private void Year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedYear = Year.SelectedItem.ToString();
            _viewModel.RefreshCharts();
        }
    }
}
