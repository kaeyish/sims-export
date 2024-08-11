using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.ViewModel
{
    public class OwnerRatingsViewModel : INotifyPropertyChanged
    {
        private User _loggedInUser;
        private List<OwnerRatingDto> _ownerRatings;
        private OwnerRatingService _service;

        public OwnerRatingsViewModel(User user)
        {
            _loggedInUser = user;
            _service = new OwnerRatingService();
            LoadOwnerRatings();
        }

        public List<OwnerRatingDto> OwnerRatingsDto
        {
            get { return _ownerRatings; }
            set
            {
                _ownerRatings = value;
                RaisePropertyChanged(nameof(OwnerRatingsDto));
            }
        }

        private void LoadOwnerRatings()
        {
            OwnerRatingsDto = _service.FindAllByOwnerId(_loggedInUser.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
