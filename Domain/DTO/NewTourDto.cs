using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.DTO
{
    public class NewTourDto
    {
        public Tour Tour { get; set; }

        public List<DateTime> Dates { get; set; }

        public NewTourDto()
        {
            Tour = new Tour();
            Dates = new List<DateTime>();
        }

        public NewTourDto(Tour tour, List<DateTime> dates)
        {
            Tour = tour;
            Dates = dates;
        }

        public NewTourDto(NewTourDto newTourDto)
        {
            Tour = newTourDto.Tour;
            Dates = newTourDto.Dates;
        }
    }
}
