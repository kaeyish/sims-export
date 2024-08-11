using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class OwnerRating : ISerializable
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public int OwnerId { get; set; }

        public int AccommodationId { get; set; }

        public int AccommodationReservationId { get; set; }

        public int Cleanliness { get; set; }

        public int OwnerCorectness { get; set; }

        public string Comment { get; set; }

        public List<String> Photos { get; set; }

        public OwnerRating () { }

        public OwnerRating(int id, int guestId, int ownerId, int accommodationId, int accommodationReservationId, int cleanliness, int ownerCorectness, string comment,List<string> photos)
        {
            Id = id;
            GuestId = guestId;
            OwnerId = ownerId;
            AccommodationId = accommodationId;
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            OwnerCorectness = ownerCorectness;
            Comment = comment;
            Photos = photos;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), OwnerId.ToString(), AccommodationId.ToString(), AccommodationReservationId.ToString(), Cleanliness.ToString(), OwnerCorectness.ToString(), Comment, string.Join(",", Photos) };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            OwnerId = int.Parse(values[2]);
            AccommodationId= int.Parse(values[3]);
            AccommodationReservationId = int.Parse(values[4]);
            Cleanliness = int.Parse(values[5]);
            OwnerCorectness = int.Parse(values[6]);
            Comment = values[7];

            string[] photos = values[8].Split(',');
            Photos = new List<string>();
            foreach (string photo in photos)
            {
                Photos.Add(photo);
            }

        }
    }
}
