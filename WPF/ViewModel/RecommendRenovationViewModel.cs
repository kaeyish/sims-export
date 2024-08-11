using BookingApp.Domain.DTO;
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
    public class RecommendRenovationViewModel
    {
        private User _loggedInUser;
        private AccommodationRenovationRecommendationService _service;
        private AccommodationReservationService _reservationService;
        private AccommodationService _accommodationService;

        public RecommendRenovationViewModel(User user)
        {
            var app = (App)Application.Current;
            _reservationService = app.AccommodationReservationService;
            _service = app.AccommodationRenovationRecommendationService;
            _accommodationService = app.AccommodationService;
            _loggedInUser = user;

        }

        public string SaveRecommendation(int reservationId, int necessity, string comment)
        {
            if(necessity<0 || necessity > 5)
            {
                return "Necesitty must be between 1-5!";
            }

            if (_service.IsIn(reservationId))
            {
                return "Vec postoji predlog za ovu rezervaciju!";
            }
            AccommodationRenovationRecommendation temp = new AccommodationRenovationRecommendation(reservationId, _accommodationService.FindById(_reservationService.FindById(reservationId).AccommodationId).OwnerId, _loggedInUser.Id, necessity, comment);
            _service.Save(temp);
            return "Uspesno dodata sugestija!";

        }



    }
}
