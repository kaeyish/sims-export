using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Guide : User
    {
        public Person GuideInfo { get; set; }

        public Guide() { }

        public void FromCSV(string[] values)
        {
            //TODO
        }

        public string[] ToCSV()
        {
            //TODO
            return new string[0];
        }

        //TODO

        /*CreateTour(string name, Locations location, string description, string language, int tourCapacity, List<string> checkPoints, DateTime start, int duration, List<string> pictures)
        StartTour()
        EndTour()
        MarkTouristArrival(string name, string checkPoint)
        MarkCheckpoint(string checkPoint)*/
    }
}
