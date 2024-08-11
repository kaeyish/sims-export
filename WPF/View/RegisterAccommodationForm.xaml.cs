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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationForm.xaml
    /// </summary>
    public partial class RegisterAccommodationForm : Window
    {
        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _repository;

        private string selectedOption;

        public RegisterAccommodationForm(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new AccommodationRepository();
        }

        // TODO: Skontati kako radio dugmici funkcionisu
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<string> photosList = new List<string>();
                foreach (string photo in Photos.Text.Split(","))
                {
                    photosList.Add(photo);
                }
                Accommodation newAccommodation = new Accommodation(LoggedInUser.Id, Name.Text, new Location(City.Text, Country.Text), (AccommodationType)Enum.Parse(typeof(AccommodationType), selectedOption), int.Parse(MaxGuestNumber.Text), int.Parse(MinReservationDays.Text), int.Parse(CancelationWindow.Text), photosList);

                _repository.Save(newAccommodation);
                MessageBox.Show("Uspesno dodato!!!");
            }catch(Exception ex)
            {
                // FormatException ako se za tipa maxGuestNumber unese string...
                MessageBox.Show("Ohh-ooooohhh greskica pri unosu !!!\n" + ex.ToString());
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null && radioButton.IsChecked == true)
            {
                selectedOption = radioButton.Content.ToString();
            }
        }

        private Accommodation GetAccommodationFromForm()
        {
            Accommodation accommodation = new Accommodation();



            return accommodation;
        }
    }
}
