using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for CreateRequest.xaml
    /// </summary>
    public partial class CreateRequest : Page
    {
        private SimpleRequestViewModel _viewModel;

        private int counter = 1;

        public CreateRequest(SimpleRequestViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility = Visibility.Visible;
            MainGrid.Visibility = Visibility.Collapsed; 

            Count.Text = '(' + counter.ToString() + '/' + PartySize.Text + ')';
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
           
            if (++counter > int.Parse(PartySize.Text)) { AddPerson.Visibility = Visibility.Collapsed; }
            else Count.Text = '(' + counter.ToString() + '/' + PartySize.Text + ')';
            _viewModel.AddPerson(PersonName.Text, PersonLastName.Text, Age.Text);
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility = Visibility.Collapsed;

            MainGrid.Visibility = Visibility.Visible;
            _viewModel.ClearPeople();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility=Visibility.Collapsed;
            MainGrid.Visibility=Visibility.Visible;
            counter = 1;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Back();
        }
    }
}
