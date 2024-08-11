using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        public Accommodation Save(Accommodation accommodation);

        public List<Accommodation> FindAll();

        public List<Accommodation> FindByParams(SearchAccommodationDTO searchAccommodationDTO);

        public Accommodation FindById(int id);

        public List<int> FindIdsByOwnerId(int ownerId);

        public bool IsAMatch(SearchAccommodationDTO searchAccommodationDTO, Accommodation accommodation);

        public int NextId();

    }
}
