using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public List<Tour> FindAll();

        public List<TourInstance> FindToursInstances(int parentId);
        public TourInstance FindInstanceById(int id);

        public TourInstance FindInstanceByDate(DateTime dateTime, int parentId);

        public List<InstancePackDto> FindUsersTours(Tourist tourist, Status? status);

        
        public Tour Save(NewTourDto newTourDto);

        public void SaveInstance(int parentId, DateTime date, int capacity);
        public Tour FindById(int id);

        public Checkpoint GetLastCheckpoint(int id);
        public List<Checkpoint> GetPreviousCheckpoints(int id);

        public List<Tour> FindByLocation(Location location, int id);

        public bool UpdateInstances(TourInstance activeInstance);

        public bool Update(Tour activeTour);
    }
}
