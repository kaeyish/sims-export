using BookingApp.Domain.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel
{
    public class ProbaVM : INotifyPropertyChanged
    {
        private readonly TourService _tourService;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }


        private ObservableCollection<Tour> _tours;

        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set { _tours = value; OnPropertyChanged("Tours"); }
        }

        public ProbaVM(User user, TourService tourService)
        {
            _tourService = tourService;
            Tours = new ObservableCollection<Tour>(tourService.FindAll());
        }

        // Example method in the view model
       
    }

}
