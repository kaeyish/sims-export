using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for CreateSegment.xaml
    /// </summary>
    public partial class CreateSegment : Page
    {
        CreateComplexRequestViewModel _viewModel;
        private int counter = 1;

        public CreateSegment(CreateComplexRequestViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

  

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            Title.Visibility = Visibility.Visible;
            _viewModel.ClearPeople();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            Title.Visibility = Visibility.Visible;
            counter = 1;
        }



        private void AddPeople_Click(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Collapsed;
            Title.Visibility = Visibility.Collapsed;
            Count.Text = '(' + counter.ToString() + '/' + PartySize.Text + ')';
        }
    }
}
