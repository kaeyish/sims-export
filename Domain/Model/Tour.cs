using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.TextFormatting;
using System.Xml.Linq;
using BookingApp.Serializer;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Domain.Model
{
    public enum Status { NotStarted, OnGoing, Finished }

    public class Tour : Serializer.ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        public int Duration { get; set; }

        public int TotalCapacity { get; set; }

        public double Rating { get; set; }

        public Location Location { get; set; }


        public List<Checkpoint> Checkpoints { get; set; }


        public List<string> Pictures { get; set; }
       
        public string? MainPicture { get; set; }
        public Status IsActive { get; set; }


        public Tour()
        {
            GuideId = -1;
            Name = string.Empty;
            Description = string.Empty;
            Language = string.Empty;
            TotalCapacity = 0;
            Rating = 0;
            Duration = 0;
            IsActive = 0;
            Location = new Location();
            Checkpoints = new List<Checkpoint>();
            Pictures = new List<string>();
        }

        public Tour(string name, int guideId, string description, string language, int totalCapacity, Location location, int duration, List<string> pictures, Status isActive, List<Checkpoint> checkpoints)
        {
            Name = name;
            GuideId = guideId;
            Description = description;
            Language = language;
            TotalCapacity = totalCapacity;
            Location = location;
            Duration = duration;
            IsActive = isActive;
            Rating = 0.00;
            Checkpoints = new List<Checkpoint>(checkpoints);
            Pictures = new List<string>(pictures);
            if (Pictures[0] != "")
                MainPicture = "/Resources/Images/" + Pictures[0];
        }


        public Tour(Tour tour)
        {
            Id = tour.Id;
            GuideId = tour.GuideId;
            Name = tour.Name;
            Description = tour.Description;
            Location = tour.Location;
            Language = tour.Language;
            TotalCapacity = tour.TotalCapacity;
            Duration = tour.Duration;
            Rating = tour.Rating;
            IsActive = tour.IsActive;
            Checkpoints = new List<Checkpoint>(tour.Checkpoints);
            Pictures = new List<string>(tour.Pictures);
            if (Pictures[0] != "")
                MainPicture = "/Resources/Images/" + Pictures[0];
          
        }

        public string[] ToCSV()
        {
            string pictures = string.Join(",", Pictures);
            string checkpoints = GenerateString(Checkpoints);


            string[] csvValues = { Id.ToString(), GuideId.ToString(),Name, Description, Location.City,Location.Country,
                                Language.ToString(), TotalCapacity.ToString(), Duration.ToString(), Rating.ToString(),
                                IsActive.ToString(), pictures, checkpoints};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            Name = values[2];
            Description = values[3];
            Location.City = values[4];
            Location.Country = values[5];
            Language = values[6];
            TotalCapacity = Convert.ToInt32(values[7]);
            Duration = Convert.ToInt32(values[8]);
            Rating = Convert.ToDouble(values[9]);
            IsActive = (Status)Enum.Parse(typeof(Status), values[10]);
            Pictures.AddRange(values[11].Split(","));

            string[] data = values[12].Split(";");
            foreach (string checkpoint in data)
            {
                Checkpoint temp = new Checkpoint();
                Checkpoints.Add(temp.FromCSV(checkpoint));
            }
            if (Pictures[0]!="")
                MainPicture = "/Resources/Images/" + Pictures[0];
         

        }

        public List<DateTime> convertDates(string[] source)
        {
            List<DateTime> result = new List<DateTime>();
            foreach (string s in source)
            {
                result.Add(Convert.ToDateTime(s));
            }
            return result;
        }


        public string GenerateString<T>(List<T> source)
        {
            List<string> result = new List<string>();
            foreach (T member in source)
            {
                if (member == null) return "";
                result.Add(member.ToString());
            }

            return string.Join(";", result);
        }

        public override string ToString()
        {
            return Name + " " + Location.ToString() + " " + Description + " " + Language + " " + TotalCapacity + " " + IsActive;
        }


    }
}