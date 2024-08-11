using BookingApp.Domain.Model;
using BookingApp.DTO;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class AccommodationService
    {
        private readonly AccommodationRepository _repository = new();

        public AccommodationService() { }

        public Accommodation FindById(int id)
        {
            return _repository.FindById(id); 
        }


    }
}
