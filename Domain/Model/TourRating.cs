using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRating : ISerializable
    {

        public double OverallRating { get; set; }

        public double GuideKnowledge { get; set; }

        public double GuideLanguageFLuency { get; set; }

        public double Amusement { get; set; }

        public int Id { get; set; }
        public int TourId { get; set; }

        public int TouristId { get; set; }

        public bool IsValid { get; set; }

        public string Comment { get; set; }

        public List<string> Pictures { get; set; }

        public TourRating()
        {
            Id = -1;
            TourId = -1;
            TouristId = -1;
            GuideKnowledge = 0.00;
            GuideLanguageFLuency = 0.00;
            Amusement = 0.00;
            OverallRating = 0.00;
            Comment = string.Empty;
            Pictures = new List<string>();
            IsValid = true;
        }

        public TourRating(int tourId, int touristId, double knowledge, double language, double amusement, string comment, List<string> pictures)
        {
            TourId = tourId;
            TouristId = touristId;
            GuideKnowledge = knowledge;
            GuideLanguageFLuency = language;
            Amusement = amusement;
            Pictures = pictures;
            Comment = comment;
            OverallRating = (knowledge + language + amusement) / 3;
            IsValid = true;
        }

        public double CalculateOverall()
        {
            return Math.Round((GuideKnowledge + GuideLanguageFLuency + Amusement) / 3, 2);
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            TouristId = int.Parse(values[2]);
            OverallRating = double.Parse(values[3]);
            GuideKnowledge = double.Parse(values[4]);
            GuideLanguageFLuency = double.Parse(values[5]);
            Amusement = double.Parse(values[6]);
            Comment = values[7];
            IsValid = bool.Parse(values[8]);
            Pictures.AddRange(values[9].Split(","));
        }

        public string[] ToCSV()
        {
            string pictures = string.Join(",", Pictures);
            string[] csvValues = {Id.ToString(), TourId.ToString(), TouristId.ToString(),
                                OverallRating.ToString(), GuideKnowledge.ToString(),
                                GuideLanguageFLuency.ToString(), Amusement.ToString(),
                                Comment, IsValid.ToString(), pictures};
            return csvValues;
        }
    }
}
