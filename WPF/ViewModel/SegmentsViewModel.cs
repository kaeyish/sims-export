using BookingApp.Domain.Model;
using BookingApp.Service;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{

    class SegmentsViewModel : INotifyPropertyChanged
    {
        private SimpleTourRequestService _service;

        private User _user;

        private ObservableCollection<SimpleTourRequest> _requests{get;set;}
        
        public ObservableCollection<SimpleTourRequest> Requests {
            get { return _requests; }
            set { _requests = value; OnPropertyChanged("Requests"); }
        }

        private Frame _frame;

       public SegmentsViewModel(User user, List<int> ids, Frame frame)
        {
            //nova servis metoda koja vuce sve sa idevima uwu
            var app = (App)Application.Current;
            _service = app.SimpleTourRequestService;

            Requests = new ObservableCollection<SimpleTourRequest>(_service.GetBulk(ids));

            _user = user;
            _frame = frame;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

        internal void Back()
        {
            Requests requests = new Requests(_user, _frame);
            _frame.Navigate(requests);
        }
    }
}
