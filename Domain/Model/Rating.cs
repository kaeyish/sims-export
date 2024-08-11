using BookingApp.Serializer;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BookingApp.Model
{
    public class Rating : ISerializable
    {
        public int Id { get; set; }

        public string GuestName { get; set; }

        public string OwnerName { get; set; }

        public int AccommodationReservationId { get; set; }

        public int Cleanliness { get; set; }

        public int RuleFollowing { get; set; }

        public string Comment { get; set; }

        public Rating() { }

        public Rating(string guestName, string ownerName, int accommodationReservationId, int cleanliness, int ruleFollowing, string comment)
        {
            GuestName = guestName;
            OwnerName = ownerName;
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            RuleFollowing = ruleFollowing;
            Comment = comment;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestName, OwnerName, AccommodationReservationId.ToString(), Cleanliness.ToString(), RuleFollowing.ToString(), Comment};
            return csvValues;
        }

        public void FromCSV(string[] values) {
            Id = int.Parse(values[0]);
            GuestName = values[1];
            OwnerName = values[2];
            AccommodationReservationId = int.Parse(values[3]);
            Cleanliness = int.Parse(values[4]);
            RuleFollowing = int.Parse(values[5]);
            Comment = values[6];
        }

        public override string ToString()
        {
            return $"{Id}, {GuestName}, {OwnerName}, {AccommodationReservationId}, {Cleanliness}, {RuleFollowing}, {Comment}";
        }


    }
}
