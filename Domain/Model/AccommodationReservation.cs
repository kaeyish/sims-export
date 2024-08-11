using BookingApp.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Model
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public int AccommodationId { get; set; }

        public int ReservationDays { get; set; }

        public int GuestNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public AccommodationReservation()
        {
        }

        public AccommodationReservation(int guestId, int accommodationId, int reservationDays, int guestNumber, DateTime startDate, DateTime endDate)
        {
            GuestId = guestId;
            AccommodationId = accommodationId;
            ReservationDays = reservationDays;
            GuestNumber = guestNumber;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), AccommodationId.ToString(), ReservationDays.ToString(), GuestNumber.ToString(), StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd")};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);
            ReservationDays = int.Parse(values[3]);
            GuestNumber = int.Parse(values[4]);
            StartDate = Convert.ToDateTime(values[5]);
            EndDate = Convert.ToDateTime(values[6]);

        }

        public override string? ToString()
        {
            return Id + " " + GuestId + " " + AccommodationId + " " + ReservationDays.ToString() + " " + GuestNumber.ToString() + " " + StartDate.ToString("yyyy-MM-dd") + " " + EndDate.ToString("yyyy-MM-dd");
        }

        public bool InRange(DateTime date)
        { 
            return (date >= StartDate) && (date <= EndDate);
        }
    }
}
