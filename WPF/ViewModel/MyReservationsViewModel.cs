using System;
using System.Collections.Generic;
using System.ComponentModel;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Service;
using BookingApp.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookingApp.Domain.Model;

namespace BookingApp.ViewModel
{
    public class MyReservationsViewModel : INotifyPropertyChanged
    {﻿
        private User _loggedInUser;
        private List<AccommodationReservationDto> _reservations;
        private AccommodationReservationService _reservationService;
        private AccommodationService _accommodationService;
        private OwnerRatingService _ownerRatingService;


        public MyReservationsViewModel(User user)
        {
            _loggedInUser = user;
            _reservationService = new AccommodationReservationService();
            LoadGuestReservations();
        }

        public List<AccommodationReservationDto> Reservations
        {
            get { return _reservations; }
            set
            {
                _reservations = value;
                RaisePropertyChanged(nameof(Reservations));
            }
        }

        private void LoadGuestReservations()
        {
            Reservations = _reservationService.FindAllByGuestId(_loggedInUser.Id);
        }

        public bool SubmitRating(int reservationId, int cleanlinessRating, int ownerBehavior, String additionalComment, String photos)
        {
            _accommodationService = new AccommodationService();
            _ownerRatingService = new OwnerRatingService();
            AccommodationReservation reservation = _reservationService.FindById(reservationId);
            DateTime thisDay = DateTime.Today;
            if (thisDay > reservation.EndDate && thisDay <= reservation.EndDate.AddDays(5))
            {
                if (_ownerRatingService.IsAlreadyRated(reservationId))
                {
                    return false;
                }

                string[] photo = photos.Split(',');
                List<string> PhotosPath = new List<string>();
                foreach (string phototo in photo)
                {
                    PhotosPath.Add(phototo);
                }

                OwnerRating ownerRating = new OwnerRating(0, _loggedInUser.Id, _accommodationService.FindById(reservation.AccommodationId).OwnerId, reservation.AccommodationId, reservation.Id, cleanlinessRating, ownerBehavior, additionalComment, PhotosPath);
                _ownerRatingService.Save(ownerRating);
                return true;
            }

            return false;
        }

        public String CancelReservation(string reservationId)
        {
            _accommodationService = new AccommodationService();
            AccommodationReservation reservation = _reservationService.FindById(int.Parse(reservationId));
            DateTime thisDay = DateTime.Today;
            if (thisDay < reservation.StartDate.AddDays(-(_accommodationService.FindById(reservation.AccommodationId).CancelationWindow)))
            {
                _reservationService.DeleteById(int.Parse(reservationId));
                LoadGuestReservations();    
                return "Uspesno otkazana rezervacija!";
            }

            return "Rezervacija nije otkazana!";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
