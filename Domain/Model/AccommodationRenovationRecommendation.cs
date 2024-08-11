using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookingApp.Serializer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Domain.Model
{
    public class AccommodationRenovationRecommendation : ISerializable
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public int GuestId { get; set; }

        public int OwnerId { get; set; }

        public int NecessityLevel { get; set; }

        public string Comment { get; set; }

        public AccommodationRenovationRecommendation() { }

        public AccommodationRenovationRecommendation(int reservationId, int guestId, int ownerId, int necessityLevel, string comment)
        {
            ReservationId = reservationId;
            GuestId = guestId;
            OwnerId = ownerId;
            NecessityLevel = necessityLevel;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ReservationId.ToString(), OwnerId.ToString(), GuestId.ToString(), NecessityLevel.ToString(), Comment};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId= int.Parse(values[1]);
            OwnerId = int.Parse(values[2]);
            GuestId = int.Parse(values[3]);
            NecessityLevel = int.Parse(values[4]);
            Comment = values[5];
        }
    }
}
