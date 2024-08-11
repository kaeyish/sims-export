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
    /// Interaction logic for Segments.xaml
    /// </summary>
    public partial class Segments : Page
    {
        private SegmentsViewModel _viewModel;
        public Segments(User user, List<int> ids, Frame frame, string? Name = null)
        {
            InitializeComponent();
            _viewModel = new SegmentsViewModel(user, ids, frame);
            ReqName.Text = Name;
            DataContext = _viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Back();
        }
    }
}
