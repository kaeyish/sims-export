using BookingApp.Serializer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using BookingApp.Model;

namespace BookingApp.Domain.Model
{
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public AccommodationType AccommodationType { get; set; }
        public int MaxGuestNumber { get; set; }
        public int MinReservationDays { get; set; }
        public int CancelationWindow { get; set; }
        List<string> Photos { get; set; }

        public Accommodation()
        {
            AccommodationType = AccommodationType.None;
        }

        public Accommodation(int ownerId, string name, Location location, AccommodationType accommodationType, int maxGuestNumber, int minReservationDays, int cancelationWindow, List<string> photos)
        {
            OwnerId = ownerId;
            Name = name;
            Location = location;
            AccommodationType = accommodationType;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            CancelationWindow = cancelationWindow;
            Photos = photos;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), OwnerId.ToString(), Name, Location.City, Location.Country, ((int)AccommodationType).ToString(), MaxGuestNumber.ToString(), MinReservationDays.ToString(), CancelationWindow.ToString(), string.Join(",", Photos) };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            OwnerId = int.Parse(values[1]);
            Name = values[2];
            Location = new Location() { City = values[3], Country = values[4] };
            AccommodationType = (AccommodationType)int.Parse(values[5]);
            MaxGuestNumber = int.Parse(values[6]);
            MinReservationDays = int.Parse(values[7]);
            CancelationWindow = int.Parse(values[8]);


            string[] photos = values[9].Split(',');
            Photos = new List<string>();
            foreach (string photo in photos)
            {
                Photos.Add(photo);
            }
        }

        public bool IsDateReserved(DateTime Date)
        {
            AccommodationReservationRepository _reservations = new AccommodationReservationRepository();
            List<AccommodationReservation> reservations = _reservations.FindAllByAccommodationId(Id);

            foreach (AccommodationReservation reservation in reservations)
            {
                if (reservation.InRange(Date))
                {
                    return true;
                }
            }

            return false;
        }

        public List<DateTime> GetAvailableDates(DateTime startDate, DateTime EndDate, int reservationDays)
        {
            List<DateTime> freeDates = new List<DateTime>();

            DateTime currentDate = startDate;
            int consecutiveDays = 0;

            while (currentDate <= EndDate)
            {
                if (!IsDateReserved(currentDate))
                {
                    consecutiveDays++;
                    if (consecutiveDays == reservationDays)
                    {
                        for (int i = 0; i < reservationDays; i++)
                        {
                            freeDates.Add(currentDate.AddDays(-i));
                        }
                    }

                    if (consecutiveDays > reservationDays)
                    {
                        freeDates.Add(currentDate);
                    }
                }
                else
                {
                    consecutiveDays = 0;
                }
                currentDate = currentDate.AddDays(1);
            }

            if (!freeDates.Any())
            {
                return GetAvailableDates(startDate.AddDays(-10), EndDate.AddDays(10), reservationDays);
            }

            return freeDates;
        }

        public override string? ToString()
        {

            return Id.ToString() + ", " + OwnerId.ToString() + ", " + Name + ", " + Location.City + ", " + Location.Country + ", " + AccommodationType.ToString() + ", " + MaxGuestNumber.ToString() + ", " + MinReservationDays.ToString() + ", " + CancelationWindow.ToString();
        }

        public bool HasOneOfIds(List<int> ids)
        {
            foreach (int id in ids)
            {
                if (Id == id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
