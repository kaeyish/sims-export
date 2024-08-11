using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Service
{
    public class RenovationService
    {
        private readonly IRenovationRepository _repository;

        public RenovationService(IRenovationRepository repository)
        {
            _repository = repository;
        }

        public List<Renovation> FindAll()
        {
            return _repository.FindAll();
        }

        public List<Renovation> FindAllByOwnerId(int id)
        {
            return FindAll().Where(r => r.OwnerId == id).ToList();
        }

        public void DeleteById(int id)
        {
            if(_repository.FindById(id).StartDate >= DateTime.Now.AddDays(5))
            {
                _repository.DeleteById(id);
            }
            else
            {
                MessageBox.Show("Uh-oh greska");
            }
            
        }

        public Renovation FindById(int id)
        {
            return _repository.FindById(id);
        }
    }
}
