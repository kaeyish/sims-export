using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Tourist : Person, ISerializable
    {
        public int Id { get; set; }
        public List<int> MyTours { get; set; }

        public Tourist()
        {
            Id = 0;
            MyTours = new List<int>();
        }

        public Tourist(int id, string name, string lastName, int age)
        {
            Id = id;
            MyTours = new List<int>();
            FirstName = name;
            LastName = lastName;
            Age = age;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            FirstName = values[1];
            LastName = values[2];
            Age = int.Parse(values[3]);
            string[] TourIds = values[4].Split(",");
            foreach (string TourId in TourIds)
                MyTours.Add(int.Parse(TourId));

        }

        public string[] ToCSV()
        {
            string tours = string.Join(",", MyTours);

            string[] csvValues = { Id.ToString(), FirstName, LastName, Age.ToString(), tours };

            return csvValues;
        }
    }
}
