using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Packaging;
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
    /// Interaction logic for CreateTourForm.xaml
    /// </summary>
    public partial class CreateTourForm : Window

    {
        public User LoggedInUser { get; set; }

        private readonly TourRepository _tourRepository;

        public CreateTourForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> pictures = new List<string>();
                List<Checkpoint> checkpoints = new List<Checkpoint>();
                List<DateTime> datesTime= new List<DateTime>();
                List<Person> tourists = new List<Person>();

                //tourists.Add(new Person());

               foreach (string picture in Pictures.Text.Split(','))
                {
                    pictures.Add(picture);
                }
                
                foreach (string check in Checkpoints.Text.Split(","))
                {
                    Checkpoint checkpoint = new Checkpoint(check);
                    checkpoints.Add(checkpoint);
                }

                foreach (string date in Date.Text.Split(","))
                {
                    datesTime.Add(DateTime.Parse(date));
                }

                Tour newTour = new Tour(Name.Text, LoggedInUser.Id, Description.Text, Language.Text, int.Parse(MaxTouristsNumber.Text), new Location(City.Text, Country.Text), int.Parse(DurationBox.Text), pictures, Status.NotStarted, checkpoints);

                NewTourDto newTourDto = new NewTourDto(newTour, datesTime);

                _tourRepository.Save(newTourDto);
                MessageBox.Show("Uspesan unos!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska pri unosu!" + ex.ToString());
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GuideForm guideWindow = new GuideForm();
            guideWindow.Show();
            Close();
        }
    }
}
