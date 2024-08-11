using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Domain.Model
{
    public class ComplexTourRequest : ISerializable
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int NumberOfSegments { get; set; }

        public DateTime StartDate { get; set; }

        public State Status { get; set; }

        public List<int> Segments { get; set; }

        public int UserId { get; set; }

        public ComplexTourRequest() {
            Id = 0;
            Name = String.Empty;
            Description = String.Empty;
            NumberOfSegments = 0;
            StartDate = DateTime.MinValue;
            Status = State.Pending;
            Segments = new List<int>();
            UserId = -1;
        }

        public ComplexTourRequest(string name, string description, DateTime startDate, List<int> ids, int userId) {
            Id = 0;
            Name = name;
            Description = description;
            StartDate = startDate;
            Segments = ids;
            Status = State.Pending;
            UserId = userId;
            NumberOfSegments = ids.Count;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Description = values[2];
            StartDate = DateTime.Parse(values[3], CultureInfo.InvariantCulture);
            Status = (State) Enum.Parse(typeof(State), values[4]);

            NumberOfSegments = int.Parse(values[5]);    

            string[] temp = values[6].Split(',');

            Segments = new List<int>();

            foreach (string s in temp)
            {
                Segments.Add(int.Parse(s));
            }

            UserId = int.Parse(values[7]);

        }

        public string[] ToCSV()
        {

            string segments = string.Join(",", Segments);

            string[] csvVals = {Id.ToString(), Name, Description, StartDate.ToString(CultureInfo.InvariantCulture), Status.ToString(), NumberOfSegments.ToString(), segments, UserId.ToString()};

            return csvVals;
        }
    }
}
