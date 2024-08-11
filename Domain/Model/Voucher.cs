using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }

        public int TouristId { get; set; }

        public int? GuideId { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public DateOnly ExpiryDate { get; set; }

        public Voucher()
        {
            Id = 0;
            TouristId = 0;
            GuideId = null;
            Name = string.Empty;
            Source = string.Empty;
            ExpiryDate = DateOnly.MinValue;
        }


        public Voucher(int id, int touristid, string name, string source, DateOnly expiryDate, int? guideid = null)
        {
            Id = id;
            TouristId = touristid;
            GuideId = guideid;
            Name = name;
            Source = source;
            ExpiryDate = expiryDate;
        }

        public Voucher(Voucher voucher)
        {
            Id = voucher.Id;
            TouristId = voucher.TouristId;
            GuideId = voucher.GuideId;
            Name = voucher.Name;
            Source = voucher.Source;
            ExpiryDate = voucher.ExpiryDate;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            Name = values[2];
            Source = values[3];
            ExpiryDate = DateOnly.Parse(values[4], (CultureInfo.InvariantCulture));
            if (values[5] == string.Empty)
                GuideId = null;
            else GuideId = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TouristId.ToString(), Name, Source, ExpiryDate.ToString((CultureInfo.InvariantCulture)), string.Empty };
            if (GuideId.HasValue)
                csvValues[5] = GuideId.Value.ToString();
            else csvValues[5] = string.Empty;

            return csvValues;
        }

        override public string ToString()
        {
            return Id + " " + Name + " " + ExpiryDate.ToString() + " " + Source;
        }
    }
}
