using BookingApp.Domain.Model;
using BookingApp.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModel
{
    public class GuestMyRatingsViewModel
    {
        private RatingService _service;

        public GuestMyRatingsViewModel()
        {
            var app = (App)Application.Current;
            _service = app.RatingService;
        }

        public List<Rating> GetRatings(int guestId)
        {
            return _service.FindAllSuitableByGuestId(guestId);
        }
    }
}
