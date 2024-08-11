using BookingApp.Domain.Model;
using BookingApp.Repository;
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
    /// Interaction logic for TrackTourForm.xaml
    /// </summary>
    public partial class TrackTourForm : Window
    {
        public User LoggedInUser { get; set; }

        private readonly TourRepository _tourRepository;
        public TrackTourForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            GuideForm guideWindow = new GuideForm();
            guideWindow.Show();
            Close();
        }
        
        private void ShowTourButton_Click(object sender, RoutedEventArgs e)
        {
          /*  if (DateTime.TryParse(DateBox.Text, out DateTime date))
            {
                List<Tour> tours = _tourRepository.FindByDate(date);
                ResultListBox.ItemsSource = tours;
            } else
            {
                MessageBox.Show("Wrong date format!!");
            }*/
        }

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultListBox.SelectedItem != null)
            {
                Tour selectedTour = (Tour)ResultListBox.SelectedItem;
                if (selectedTour.IsActive == Status.NotStarted)
                {
                    selectedTour.IsActive = Status.OnGoing; // tura je aktivna
                    MessageBox.Show("You started tour!!");
                    CheckpointListBox.ItemsSource = selectedTour.Checkpoints;
                    selectedTour.Checkpoints[0].IsChecked = true;
                    _tourRepository.Update(selectedTour);
                }
                else
                {
                    MessageBox.Show("Tour has already started!!"); 
                }
            }
        }

        private void EndTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResultListBox.SelectedItem != null)
            {
                Tour selectedTour = (Tour)ResultListBox.SelectedItem;
                if (selectedTour.IsActive == Status.OnGoing)
                {
                    selectedTour.IsActive = Status.OnGoing;
                    MessageBox.Show("You ended tour!!");
                    selectedTour.Checkpoints[selectedTour.Checkpoints.Count - 1].IsChecked = true;
                    _tourRepository.Update(selectedTour);
                }
                else
                {
                    MessageBox.Show("Tour has already ended!!");
                }
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckpointListBox.SelectedItem != null)
            {
                Checkpoint selectedCheckppint = (Checkpoint)CheckpointListBox.SelectedItem;
                if (!selectedCheckppint.IsChecked) { 
                    selectedCheckppint.IsChecked = true;
                    MessageBox.Show("Marked as visited!!");
                }
                else
                {
                    MessageBox.Show("Ccheckpoint has already been visited!!");
                }
            }
        }
    }
}
