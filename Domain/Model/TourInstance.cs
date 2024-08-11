using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BookingApp.Domain.Model
{
    public class TourInstance : Serializer.ISerializable
    {
        public int Id;


        public int ParentId;

        public DateTime Date;

        public int AvailableCapacity;

        public Status Status;

        public List<int> ReservationIds;

        public TourInstance()
        {
            ReservationIds = new List<int>();
            ParentId = 0;
            AvailableCapacity = 0;
            Date = DateTime.MinValue;
        }

        public TourInstance(List<int> ids, int parentId, int availableCapacity, DateTime date, Status status)
        {
            ReservationIds = ids;
            ParentId = parentId;
            AvailableCapacity = availableCapacity;
            Date = date;
            Status = status;
        }

        public TourInstance(TourInstance tourInstance)
        {
            ReservationIds = tourInstance.ReservationIds;
            AvailableCapacity = tourInstance.AvailableCapacity;
            Date = tourInstance.Date;
            ParentId = tourInstance.ParentId;
            Status = tourInstance.Status;
        }

        override public string ToString() { return Date.ToString() + " " + AvailableCapacity.ToString(); }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            ParentId = Convert.ToInt32(values[1]);
            AvailableCapacity = Convert.ToInt32(values[2]);
            Date = Convert.ToDateTime(values[3], (CultureInfo.InvariantCulture));
            Status = (Status)Enum.Parse(typeof(Status), values[4]);
            string[] data = values[5].Split(',');

            if (values[5] != "")
            foreach (string id in data)
            {
                ReservationIds.Add(int.Parse(id));
            }


        }


        public string[] ToCSV()
        {

            string result = string.Join(",", ReservationIds);

            string[] csvValues = { Id.ToString(), ParentId.ToString(), AvailableCapacity.ToString(), Date.ToString((CultureInfo.InvariantCulture)), Status.ToString(), result };
            return csvValues;
        }


    }
}
