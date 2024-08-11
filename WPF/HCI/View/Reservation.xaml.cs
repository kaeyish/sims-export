using BookingApp.Domain.Model;
using BookingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class Reservation : Page
    {

        private TourReservationViewModel _viewModel;

        private int counter = 1;

        public Reservation(User user, Frame frame, Tour tour)
        {
            InitializeComponent();
         _viewModel = new TourReservationViewModel(user, tour, frame);
            DataContext = _viewModel;
        }

        private void Dates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedDate(Dates.SelectedItem.ToString());
        }

        private void AddPeople_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Collapsed;
            PersonGrid.Visibility = Visibility.Visible;
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
            _viewModel.ClearPeople();
            counter = 1;
            MainGrid.Visibility = Visibility.Visible;
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            PersonGrid.Visibility = Visibility.Collapsed;
            MainGrid.Visibility = Visibility.Visible;
            counter = 1;
        }

        private void ReservationCancel_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Back();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Confirm();
        }

    }

}
