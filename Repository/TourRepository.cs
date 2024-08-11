using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRepository : ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private const string FilePathInstances = "../../../Resources/Data/instances.csv";

        private readonly Serializer<Tour> _serializer;

        private readonly Serializer<TourInstance> _serializerInstance;

        private List<Tour> _tours;

        private List<TourInstance> _allInstances;


        public TourRepository()
        {
            _serializer = new Serializer<Tour>();
            _serializerInstance = new Serializer<TourInstance>();
            
            _tours = _serializer.FromCSV(FilePath);
            _allInstances = _serializerInstance.FromCSV(FilePathInstances);
        }

        public List<Tour> FindAll() { 
            return _tours;
        }

        public List<TourInstance> FindToursInstances(int parentId) {
            List<TourInstance> result = new List<TourInstance>();

            foreach (TourInstance instance in _allInstances) {
                if (instance.ParentId == parentId) {
                    result.Add(instance);
                }
            }
            return result;
        }

        public List<Checkpoint> GetPreviousCheckpoints(int id) {
            List<Checkpoint> checkpoints = FindById(id).Checkpoints;
            checkpoints.Remove(GetLastCheckpoint(id));

            return checkpoints.FindAll(c => c.IsChecked);
        }

        public Checkpoint GetLastCheckpoint(int id) {
            Tour tour = FindById(id);

            return tour.Checkpoints.Last(c => c.IsChecked);
        }


     public TourInstance FindInstanceById(int id) {
            return _allInstances.FirstOrDefault(u => u.Id == id);
        }

        public TourInstance FindInstanceByDate(DateTime dateTime, int parentId) {
            List<TourInstance> _singleTourInstances = FindToursInstances(parentId);


            foreach (TourInstance instance in _singleTourInstances)
            {
                if (instance.Date == dateTime) {
                    return instance; 
                }
            }
            return null;
        }

        public List <TourInstance> FindBulk(List<int> ids) {
            List<TourInstance> eligible = new List<TourInstance>();

            foreach (int id in ids)
            {
                eligible.Add(FindInstanceById(id));
            }
            return eligible;
        }

     

        public List<InstancePackDto> FindUsersTours(Tourist tourist, Status? status) {

            List<InstancePackDto> eligible = new List<InstancePackDto>();

            TourReservationRepository _repo = new();

            List<TourInstance> instances = FindBulk(_repo.ExtractTourIds(tourist.Id));


            if (status.HasValue)
            foreach (TourInstance instance in instances) {
                    if (instance.Status == status)
                   eligible.Add(new InstancePackDto (FindById(instance.ParentId) , instance));
            }
            else
            foreach(TourInstance instance in instances) {
                    eligible.Add(new InstancePackDto(FindById(instance.ParentId), instance));
            }

            return eligible;
        }

        public Tour Save(NewTourDto newTourDto)
        {
            int id = NextId();
            newTourDto.Tour.Id = id;
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(newTourDto.Tour);
            _serializer.ToCSV(FilePath, _tours);

            foreach (DateTime date in newTourDto.Dates)
                SaveInstance(id, date, newTourDto.Tour.TotalCapacity);
                

            return newTourDto.Tour;
        }

        public void SaveInstance(int parentId, DateTime date, int capacity) {
            TourInstance _instance = new TourInstance();
            _instance.ParentId = parentId;
            _instance.Id = NextIdInstance();
            _instance.Date = date;
            _instance.AvailableCapacity = capacity;

            _allInstances.Add(_instance);
            _serializerInstance.ToCSV(FilePathInstances, _allInstances);

        }

        private int NextIdInstance()
        {
            if (_allInstances.Count < 1)
            {
                return 1;
            }
            return _allInstances.Max(t => t.Id) + 1;
        }


        private int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.Id) + 1;
        }

        public Tour FindById(int id) { 
            foreach (Tour tour in _tours) {
                if (tour.Id == id){
                    
                    return tour;
                }
            }
            return null;
        }

        public List<Tour> FindByLocation(Location location, int id) {
            List<Tour> result = new List<Tour>();
            foreach(Tour tour in _tours)
                if (tour.Location.Equals(location) && tour.Id!=id)
                    result.Add(tour);
            return result;
        }

        public bool UpdateInstances(TourInstance activeInstance)
        {

            _allInstances.Remove(activeInstance);
                List<TourInstance> tempTours = new List<TourInstance>();
                foreach (TourInstance instance in _allInstances)
                {
                    if (instance.Id == activeInstance.Id)
                    {
                        tempTours.Add(activeInstance);
                    }
                    else tempTours.Add(instance);
                }
                _allInstances = tempTours;

                try
                {
                _serializerInstance.ToCSV(FilePathInstances, _allInstances);
            }
            catch (Exception ex) { return false; }
            return true;
        }


        public bool Update(Tour activeTour)
        {
            List<Tour> tempTours = new List<Tour>();
            foreach (Tour tour in _tours) {
                if(tour.Id == activeTour.Id){
                    tempTours.Add(activeTour);
                }
                else tempTours.Add(tour);
            }
            _tours = tempTours;

            try{
                _serializer.ToCSV(FilePath, _tours);
            } catch (Exception ex) { return false; }
            return true;
        }

    }
}