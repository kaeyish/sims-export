using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRecommendationRepository
    {
        public List<AccommodationRenovationRecommendation> FindAll();

        public void Save(AccommodationRenovationRecommendation recommendation);

        public void Update(AccommodationRenovationRecommendation recommendation);
    }
}
