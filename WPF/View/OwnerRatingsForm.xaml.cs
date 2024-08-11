using BookingApp.Domain.Model;
using BookingApp.Model;
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
using System.Windows.Shapes;

namespace BookingApp.View
{
    public partial class OwnerRatingsForm : Window
    {

        private User _loggedUser;

        private OwnerRatingsViewModel _viewModel;

        public OwnerRatingsForm(User user)
        {
            InitializeComponent();
            _loggedUser = user;
            _viewModel = new OwnerRatingsViewModel(_loggedUser);
            DataContext = _viewModel;
        }
    }
}
