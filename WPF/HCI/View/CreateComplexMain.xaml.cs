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
    /// Interaction logic for CreateComplexMain.xaml
    /// </summary>
    public partial class CreateComplexMain : Page
    {

        private CreateComplexRequestViewModel _viewModel;

        public CreateComplexMain(User user, Frame frame)
        {
            InitializeComponent();
            _viewModel = new CreateComplexRequestViewModel(user, frame);
            DataContext = _viewModel;

        }

        public CreateComplexMain(CreateComplexRequestViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;

            Name.Text = _viewModel.Name;
            Desc.Text = _viewModel.Description;

        }
    }
}
