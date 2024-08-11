using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.DTO
{
    public class InstancePackDto : INotifyPropertyChanged
    {
        private string _name { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _description { get; set; }
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged("Description"); }
        }


        private string? _picture { get; set; }

        public string? MainPicture
        {
            get { return _picture; }
            set { _picture = value; OnPropertyChanged("MainPicture"); }
        }

        private string _date{ get; set; }

        public string Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged("Date"); }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        private Location _location { get; set; }

        public Location Location
        {
            get { return _location; }
            set { _location = value; OnPropertyChanged("Location"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        public TourInstance TourInstance { get; set; }

        public InstancePackDto()
        {
            Name = string.Empty;
            Description = string.Empty;
            MainPicture = string.Empty;
            Location = new Location();
            TourInstance = new TourInstance();
            Date = TourInstance.Date.ToShortDateString();
            Status = "NotStarted";
        }

        public InstancePackDto(Tour tour, TourInstance instance)
        {
            Name = tour.Name;
            Description = tour.Description;
            Location = tour.Location;
            if (tour.MainPicture != "")
            MainPicture =  tour.MainPicture;
            TourInstance = instance;
            Date = TourInstance.Date.ToShortDateString();
            Status = TourInstance.Status.ToString();
        }

        override public string ToString()
        {
            return Name + " " + TourInstance.ToString();
        }

    }
}
