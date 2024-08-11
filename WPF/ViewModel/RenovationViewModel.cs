using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel
{
    public class RenovationViewModel
    {
        private User _loggedInUser;
        private List<Renovation> _renovations;
        private RenovationService _service;

        public RenovationViewModel(User user)
        {
            _loggedInUser = user;
            var app = (App)Application.Current;
            _service = app.RenovationService;
            LoadRenovations();
        }

        public List<Renovation> Renovations
        {
            get { return _renovations; }
            set
            {
                _renovations = value;
                RaisePropertyChanged(nameof(Renovations));
            }
        }

        private void LoadRenovations()
        {
            Renovations = _service.FindAllByOwnerId(_loggedInUser.Id);
        }

        public void DeleteById(int id)
        {
            _service.DeleteById(id);
            Renovations.Remove(_service.FindById(id));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
