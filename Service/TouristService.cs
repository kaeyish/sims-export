using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TouristService
    {

        private readonly ITouristRepository _repository;

        public TouristService(ITouristRepository repository) {
            _repository = repository;
        }

        public List<Tourist> FindAll()
        {
            return _repository.FindAll();
        }

        public Tourist FindOne(int id)
        {
            return _repository.FindOne(id);
        }

        public void Save(Tourist tourist)
        {
            _repository.Save(tourist);

        }

        public void Update(Tourist tourist, int id) {

            tourist.MyTours.Add(id);

        }
    }
}
