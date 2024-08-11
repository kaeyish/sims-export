using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class SuperGuest : ISerializable
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public int Points { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public SuperGuest() { } 

        public SuperGuest(int guestId) 
        {
            GuestId = guestId;
            Points = 5;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddYears(1);
        }

        public bool DeductPoint()
        {
            if(Points<=0) return false;
            else
            {
                Points--;
                return true;
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), GuestId.ToString(), Points.ToString(), StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd")};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            GuestId = int.Parse(values[1]);
            Points = int.Parse(values[2]);
            StartDate = Convert.ToDateTime(values[3]);
            EndDate = Convert.ToDateTime(values[4]); 
        }


    }
}
