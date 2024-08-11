using BookingApp.Domain.Model;
using BookingApp.ViewModel;
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
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for RenovationForm.xaml
    /// </summary>
    public partial class RenovationForm : Window
    {
        private User _loggedUser;

        private RenovationViewModel _viewModel;

        public RenovationForm(User user)
        {
            InitializeComponent();
            _loggedUser = user;
            _viewModel = new RenovationViewModel(_loggedUser);
            DataContext = _viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteById(int.Parse(IdToCancel.Text));
        }
    }
}
