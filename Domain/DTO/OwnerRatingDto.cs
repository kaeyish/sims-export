using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class OwnerRatingDto
    {
        public string guestUsername {  get; set; }

        public string ownerUsername { get; set; }

        public string accommodationName { get; set; }

        public AccommodationReservation accommodationReservation { get; set; }

        public int Cleanliness { get; set; }

        public int OwnerCorectness { get; set; }

        public string Comment { get; set; }

        public List<String> Photos { get; set; }

        public OwnerRatingDto() { }

        public OwnerRatingDto(string guestUsername, string ownerUsername, string accommodationName, AccommodationReservation accommodationReservation, int cleanliness, int ownerCorectness, string comment, List<string> photos)
        {
            this.guestUsername = guestUsername;
            this.ownerUsername = ownerUsername;
            this.accommodationName = accommodationName;
            this.accommodationReservation = accommodationReservation;
            Cleanliness = cleanliness;
            OwnerCorectness = ownerCorectness;
            Comment = comment;
            Photos = photos;
        }
    }
}
