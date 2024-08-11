using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BookingApp.Model
{
    public class TourReservation : ISerializable
    {
        public int Id {get;set;}
        
        public int TouristId { get; set; }

        public int InstanceId { get; set; }

        public List<Person> People { get; set; }

        public DateTime CreatedOn { get; set; }

        public TourReservation() {
            Id = 0;
            TouristId = 0;
            InstanceId = 0;
            People = new List<Person>();
            CreatedOn = DateTime.Now;
        }    

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            InstanceId = Convert.ToInt32(values[2]);
            CreatedOn = Convert.ToDateTime(values[3], CultureInfo.InvariantCulture);
            if (values[4]!="")
            foreach (string person in values[4].Split(';'))
            {
                Person temp = new Person();
                temp = temp.FromCSV(person);
                People.Add(temp.FromCSV(person));
            }
        }

        public string[] ToCSV()
        {
            string people = string.Join(";", People);
            string[] values = {Id.ToString(), TouristId.ToString(), InstanceId.ToString(), CreatedOn.ToString(CultureInfo.InvariantCulture), people};
            return values;
        }
    }
}
