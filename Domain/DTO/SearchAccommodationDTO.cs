using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;

namespace BookingApp.Domain.DTO
{
    public class SearchAccommodationDTO
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int GuestNumber { get; set; }
        public int ReservationDays { get; set; }

        public SearchAccommodationDTO()
        {
            AccommodationType = AccommodationType.None;
            GuestNumber = 0;
        }

        public SearchAccommodationDTO(string name, Location location, AccommodationType accommodationType, int guestNumber, int reservationDays)
        {
            Name = name;
            Location = location;
            AccommodationType = accommodationType;
            GuestNumber = guestNumber;
            ReservationDays = reservationDays;
        }
    }
}
