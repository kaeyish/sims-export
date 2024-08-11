using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Domain.DTO
{
    public class SearchTourDto
    {

        public Location Location { get; set; }
        public int Duration { get; set; }
        public string Language { get; set; }
        public int PartySize { get; set; }

        public SearchTourDto()
        {
            Location = new Location();
            Duration = 0;
            Language = "";
            PartySize = 0;
        }

        public SearchTourDto(Location location, int duration, string language, int partySize)
        {
            Location = location;
            Duration = duration;
            Language = language;
            PartySize = partySize;
        }

        public SearchTourDto(SearchTourDto searchTourDto)
        {
            Location = searchTourDto.Location;
            Duration = searchTourDto.Duration;
            Language = searchTourDto.Language;
            PartySize = searchTourDto.PartySize;
        }

    }
}
