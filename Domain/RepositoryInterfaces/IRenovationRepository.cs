using BookingApp.Domain.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRepository
    {
        public List<Renovation> FindAll();

        public Renovation FindById(int id);

        public void DeleteById(int id);
    }
}
