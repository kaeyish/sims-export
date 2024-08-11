using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.DTO
{
    public class AccommodationRenovationRecommendationDTO
    {
        public int ReservationId { get; set; }

        public int NecessityLevel { get; set; }

        public string Comment { get; set; }


        public AccommodationRenovationRecommendationDTO(int reservationId, int necessityLevel, string comment)
        {
            ReservationId = reservationId;
            NecessityLevel = necessityLevel;
            Comment = comment; 
        }


    }
}
