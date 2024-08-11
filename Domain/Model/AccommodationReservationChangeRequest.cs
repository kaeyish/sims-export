using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class AccommodationReservationChangeRequest : ISerializable
    {
        public int Id { get; set; }

        public int AccommodationReservationId { get; set; }

        public DateTime NewStartDate { get; set; }

        public DateTime NewEndDate { get; set; }

        public string Status { get; set; }

        public AccommodationReservationChangeRequest() { }

        public AccommodationReservationChangeRequest(int id, int accommodationReservationId, DateTime newStartDate, DateTime newEndDate, string status)
        {
            Id = id;
            AccommodationReservationId = accommodationReservationId;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationReservationId.ToString(), NewStartDate.ToString(), NewEndDate.ToString(), Status};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservationId = int.Parse(values[1]);
            NewStartDate = Convert.ToDateTime(values[2]);
            NewEndDate = Convert.ToDateTime(values[3]);
            Status = values[4];
        }
    }
}
