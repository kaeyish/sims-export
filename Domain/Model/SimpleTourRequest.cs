using BookingApp.Model;
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

    public enum State {Pending, Invalid, Accepted};
    public enum ReqType {Simple, Complex};

    public class SimpleTourRequest : ISerializable
    {
        public int RequestId {get; set;}

        public string Name { get; set;}

        public int UserId {get; set;}

        public string Description { get; set; }

        public Location Location { get; set; }

        public string Language { get; set; }

        public List<Person> People { get; set; }

        public int PartySize { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime? SelectedDate { get; set; }

        public State State{get; set;}
        public ReqType Type{get; set;}

        public SimpleTourRequest() {
            RequestId = 0;
            UserId = 0;
            Name = string.Empty;
            Description = String.Empty;
            Location = new Location();
            Language = String.Empty;
            People = new List<Person>();
            PartySize = 0;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            SelectedDate = null;
            State = State.Pending;
            Type = ReqType.Simple;
        }

        public SimpleTourRequest(int userId, string name,string description, string city, string country, string language, List<Person> people, DateTime startDate, DateTime endDate, State state = State.Pending, ReqType type = ReqType.Simple) {
            RequestId = 0;
            UserId = userId;
            Name = name;
            Description = description;
            Location = new Location(city, country);
            Language = language;
            People = people;
            PartySize = People.Count();
            (StartDate, EndDate) = (startDate, endDate);
            SelectedDate = null;
            State = state;
            Type = type;
        }


        public void FromCSV(string[] values)
        {
            RequestId = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Name = values[2];
            Description = values[3];
            Location.City = values[4];
            Location.Country = values[5].TrimEnd();
            Language = values[6];
            PartySize = int.Parse(values[7]);

            if(values[8] != "")
            foreach (string person in values[8].Split(';'))
            {
                Person temp = new Person();
                temp = temp.FromCSV(person);
                People.Add(temp.FromCSV(person));
            }


            StartDate = DateTime.Parse(values[9], (CultureInfo.InvariantCulture));
            EndDate = DateTime.Parse(values[10], (CultureInfo.InvariantCulture));
            SelectedDate = (values[11] != string.Empty?DateTime.Parse(values[11], (CultureInfo.InvariantCulture)):null);

            State = (State)Enum.Parse(typeof(State),values[12]);
            Type = (ReqType)Enum.Parse(typeof(ReqType),values[13]);
        }

        public string[] ToCSV()
        {

            string people = String.Join(";", People);

            string selection = (SelectedDate.HasValue ? SelectedDate.Value.ToString(CultureInfo.InvariantCulture) : String.Empty);

            string[] csvValues = { RequestId.ToString(), UserId.ToString(), Name, Description, Location.City, Location.Country, Language, PartySize.ToString(), people, StartDate.ToString((CultureInfo.InvariantCulture)), EndDate.ToString((CultureInfo.InvariantCulture)), selection, State.ToString(), Type.ToString() };

            return csvValues;
        }
    }
}
