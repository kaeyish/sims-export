using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Renovation : ISerializable
    {
        public int Id {  get; set; }

        public int AccommodationId {  get; set; }

        public int OwnerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Renovation()
        {

        }

        public Renovation(int id, int accommodationId, int ownerId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            AccommodationId = accommodationId;
            OwnerId = ownerId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), AccommodationId.ToString(), OwnerId.ToString(), StartDate.ToString(), EndDate.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId= int.Parse(values[1]);
            OwnerId= int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
        }


    }
}
