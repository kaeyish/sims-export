using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.DTO
{
    public class AccommodationReservationDto
    {
        public int Id { get; set; }

        public int AccommodationId { get; set; }

        public int GuestNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }


        public AccommodationReservationDto(AccommodationReservation reservation)
        {
            Id = reservation.Id;
            AccommodationId = reservation.AccommodationId;
            GuestNumber = reservation.GuestNumber;
            StartDate = reservation.StartDate;  
            EndDate = reservation.EndDate;  
        }

        AccommodationReservationDto() { }   

    }
}
