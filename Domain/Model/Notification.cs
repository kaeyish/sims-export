using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Notification : ISerializable, INotifyPropertyChanged
    {
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        private int _id;

        public int Id {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }

        private int _userId;

        public int UserId {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged("UserId"); }
        }
        
        private int? _tourId;

        public int? TourId {
            get { return _tourId; }
            set { _tourId = value; OnPropertyChanged("TourId"); }
        }


        private string _text;

        public string Text {
            get { return _text; } set { _text = value; OnPropertyChanged("Text"); }
        }

        private DateTime _date;
        public DateTime Date {
            get { return _date; } set { _date = value; OnPropertyChanged("Date"); }
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        public Notification() {
            Id = 0;
            Text = string.Empty;
            UserId = 0;
            Date = DateTime.MinValue;
        }


        public Notification(string text, int userId, DateTime date, int? tourId = null)
        {
            Id = 0;
            Text = text;
            UserId = userId;
            Date = date;
            if (tourId != null) 
            TourId = tourId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            Text = values[2];
            Date = Convert.ToDateTime(values[3], (CultureInfo.InvariantCulture));
            if (values[4] != "")
                TourId = Convert.ToInt32(values[4]);
            else TourId = null;
        }

        public string[] ToCSV()
        {
            string[] csvvalues = { Id.ToString(), UserId.ToString(), Text, Date.ToString((CultureInfo.InvariantCulture)), TourId.ToString() };
            return csvvalues;
        }
    }
}
