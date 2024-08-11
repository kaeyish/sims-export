using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Service;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for RegisterAccommodationReservationForm.xaml
    /// </summary>
    public partial class RegisterAccommodationReservationForm : Window
    {
        public User LoggedInUser { get; set; }

        public Accommodation _accommodation { get; set; }

        public List<DateTime> dates { get; set; }

        private readonly AccommodationRepository _accommodations;

        private readonly AccommodationReservationRepository _reservations;

        private SuperGuestService _superGuestService;

        private AccommodationReservationService _reservationsService;


        public RegisterAccommodationReservationForm(User user, int accommodationId)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _accommodations = new AccommodationRepository();
            _reservations = new AccommodationReservationRepository();
            _accommodation = _accommodations.FindById(accommodationId);
            List<DateTime> dates = new List<DateTime>();
            var app = (App)Application.Current;
            _reservationsService = app.AccommodationReservationService;
            _superGuestService = app.SuperGuestService;

        }

        private void FindAvailableDates_Click(object sender, RoutedEventArgs e)
        {
            if (_accommodation.MinReservationDays > int.Parse(Duration.Text))
            {
                MessageBox.Show("Duration ne sme biti manji od minimalnog broja dana za rezervisanje!");
            }
            else
            {
                dates = _accommodation.GetAvailableDates(Convert.ToDateTime(StartDate.Text), Convert.ToDateTime(EndDate.Text), int.Parse(Duration.Text));
                updateResult(dates);
            }
        }
        
        public void CreateReservation_Click(object sender, RoutedEventArgs e) 
        {
            if (int.Parse(Guest_Number.Text) > _accommodation.MaxGuestNumber)
            {
                MessageBox.Show("Ne smes imati vise gostiju od maksimalnog broja dozvoljenih!");

            }
            else
            {

                DateTime startDate = Convert.ToDateTime(Reservation_Start_Date.Text);
                DateTime endDate = Convert.ToDateTime(Reservation_End_Date.Text);

                if (IsStartAndEndInDates(startDate, endDate) && IsConsecutiveDays(startDate, endDate))
                {
                    _reservations.Save(new AccommodationReservation(LoggedInUser.Id, _accommodation.Id, int.Parse(Duration.Text), int.Parse(Guest_Number.Text), startDate, endDate));
                    if (LoggedInUser.Role == "Guest")
                    {
                        if (_reservationsService.CountByGuestIdLastYear(LoggedInUser.Id) > 9)
                        {
                            _superGuestService.AddSuperGuest(LoggedInUser.Id);
                            LoggedInUser.Role = "SuperGuest";
                            MessageBox.Show("Upesno ste napravili rezervaciju i postali ste SuperGost!");
                        }
                        else
                        {
                            MessageBox.Show("Upesno ste napravili rezervaciju!");
                        }
                    }
                    else
                    {
                        if (_superGuestService.DeductPoint(LoggedInUser.Id))
                        {
                            MessageBox.Show("Upesno ste napravili rezervaciju sa popustom!");
                        }
                        else
                        {
                            MessageBox.Show("Upesno ste napravili rezervaciju bez popusta (nemate vise poena)!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Doslo je do greske prilikom rezervacije, datumi moraju pripadati prikazanom skupu datuma, i biti u naznacenom razmaku");
                }

            }

        }

            //funkcija za proveravanje da li uneti datumi za registraciju pripadaju skupu pronadjenih datuma
        public bool IsStartAndEndInDates(DateTime startDate, DateTime endDate)
        {
            return dates.Contains(startDate) && dates.Contains(endDate);

        }

        public bool IsConsecutiveDays(DateTime startDate, DateTime endDate)
        {
            return startDate.AddDays(int.Parse(Duration.Text)-1) == endDate;
        }


        private void updateResult(List<DateTime> results)
        {
            Results.Text = "";
            foreach (DateTime t in results)
            {
                Results.Text += t.ToString("dd/MM/yyyy") + "\n";
            }

        }

    }
}
