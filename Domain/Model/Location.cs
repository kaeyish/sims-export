using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Location
    {
        public string City { get; set; }
        public string Country { get; set; }

        public Location()
        {
            City = string.Empty;
            Country = string.Empty;
        }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public Location(Location location)
        {
            City = location.City;
            Country = location.Country;
        }

        public bool IsEmpty()
        {
            return City.Equals(string.Empty) && Country.Equals(string.Empty);
        }

        public override string ToString()
        {
            return City + ", " + Country;
        }

        public bool Equals(Location loc)
        {
            return City.Equals(loc.City) && Country.Equals(loc.Country);
        }

    }
}
