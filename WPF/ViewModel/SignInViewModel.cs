using BookingApp.Domain.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel
{
    public class SignInViewModel
    {
        private AccommodationReservationService _reservationService;
        private SuperGuestService _superGuestService;

        public SignInViewModel()
        {
            var app = (App)Application.Current;
            _reservationService = app.AccommodationReservationService;
            _superGuestService = app.SuperGuestService;
        }

        public bool CheckSuperGuest(int guestId)
        {
            if (_superGuestService.CheckExpiration(guestId))
            {
                return !_superGuestService.DeleteWithCondition(guestId, _reservationService.CountByGuestIdLastYear(guestId));
            }
            return true;
        }
    }
}
