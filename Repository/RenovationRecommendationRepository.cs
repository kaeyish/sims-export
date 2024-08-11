using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovationRecommendation.csv";

        private readonly Serializer<AccommodationRenovationRecommendation> _serializer;

        private List<AccommodationRenovationRecommendation> _recommendations;

        public RenovationRecommendationRepository()
        {
            _serializer = new Serializer<AccommodationRenovationRecommendation>();
            _recommendations = _serializer.FromCSV(FilePath);
        }

        public void Save(AccommodationRenovationRecommendation recommendation)
        {
            recommendation.Id = NextId();
            _recommendations = _serializer.FromCSV(FilePath);
            _recommendations.Add(recommendation);
            _serializer.ToCSV(FilePath, _recommendations);

        }

        public List<AccommodationRenovationRecommendation> FindAll()
        {
            return _serializer.FromCSV(FilePath);

        }

        public int NextId()
        {
            _recommendations = _serializer.FromCSV(FilePath);
            if (_recommendations.Count < 1)
            {
                return 1;
            }
            return _recommendations.Max(a => a.Id) + 1;
        }

        public void Update(AccommodationRenovationRecommendation recommendation)
        {
            List<AccommodationRenovationRecommendation> tempRecommendations = new List<AccommodationRenovationRecommendation>();
            foreach (AccommodationRenovationRecommendation r in _recommendations)
            {
                if (r.Id == recommendation.Id)
                {
                    tempRecommendations.Add(recommendation);
                }
                else tempRecommendations.Add(r);
            }
            _recommendations = tempRecommendations;
            _serializer.ToCSV(FilePath, _recommendations);

        }
    }
}
