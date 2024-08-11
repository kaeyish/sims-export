using BookingApp.Domain.Model;
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
    /// <summary>
    /// Interaction logic for GuideForm.xaml
    /// </summary>
    public partial class GuideForm : Window
    {
        public User LoggedInUser { get; set; }
        public GuideForm()
        {
            InitializeComponent();
        }

        public GuideForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourForm createTourForm = new CreateTourForm(LoggedInUser);
            createTourForm.Show();
            Close();
        }

        private void TourTrackingButtom_Click(object sender, RoutedEventArgs e)
        {
            TrackTourForm trackTourForm = new TrackTourForm(LoggedInUser);
            trackTourForm.Show();
            Close();
        }
    }
}
