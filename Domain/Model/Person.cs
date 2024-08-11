using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public enum Check {CheckedIn, Pending};
    public class Person
    {
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Check Check { get; set; }
        public string CheckedInPlace { get; set; }


        public Person() { 
            FirstName = string.Empty;
            LastName = string.Empty;
            Age = 0;
            Check = Check.Pending;
            CheckedInPlace = "NotStarted";
        }

        public Person(string name, string lastname, int age)
        {
            FirstName = name;
            LastName = lastname;
            Age = age;
            Check = Check.Pending;
            CheckedInPlace = "NotStarted";
        }

        public Person FromCSV(string source) {
            if (source == "")
                return null;
   
            FirstName = source.Split(',')[0];
            LastName = source.Split(",")[1];
            Age = Convert.ToInt32(source.Split(',')[2]);
            Check = (Check)Enum.Parse(typeof(Check), source.Split(',')[3]);
            CheckedInPlace = source.Split(",")[4];

            return this;
        }

        public override string ToString () { 
            return FirstName + "," + LastName + "," + Age + ","+ Check +"," + CheckedInPlace;
        }
    }
}
