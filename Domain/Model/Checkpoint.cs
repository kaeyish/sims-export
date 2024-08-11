using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Checkpoint
    {
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public Checkpoint()
        {
            Name = string.Empty;
            IsChecked = false;
        }
        public Checkpoint(string name)
        {
            Name = name;
            IsChecked = false;
        }

        public override string ToString()
        {
            return Name;
        }

        public Checkpoint FromCSV(string source)
        {
            if (source == "")
                return null;


            string[] temp = source.Split(",");
            Name = temp[0];
           // IsChecked = Convert.ToBoolean(temp[1]);
            return this;

        }

    }
}
