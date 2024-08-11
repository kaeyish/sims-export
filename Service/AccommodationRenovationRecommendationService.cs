using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class AccommodationRenovationRecommendationService
    {
        private IRenovationRecommendationRepository _repository;
        public AccommodationRenovationRecommendationService(IRenovationRecommendationRepository recommendationRepository)
        {
            _repository = recommendationRepository;
        }

        public List<AccommodationRenovationRecommendation> FindAll()
        {
            return _repository.FindAll();
        }


        public void Save(AccommodationRenovationRecommendation recommendation)
        {
            _repository.Save(recommendation);
        }

        public bool IsIn(int reservationId)
        {
            List<AccommodationRenovationRecommendation> temp = FindAll();
            foreach (AccommodationRenovationRecommendation r in temp)
            {
                if (r.ReservationId == reservationId)
                {
                    return true;
                }
            }

            return false;
        }


    }
}
