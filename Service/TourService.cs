using BookingApp.Domain.DTO;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class TourService
    {
        private ITourRepository _tourRepository;

        public TourService (ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public Tour FindById(int id) {
            return _tourRepository.FindById(id);
        }

        public List<Tour> FindAll()
        {
            return _tourRepository.FindAll();
        }

        public List<Tour> SearchTours(Location location, string language, string duration, string partySize)
        {
            SearchTourDto searchTourDto = ParseTours(location, language, duration, partySize);
            List<Tour> eligible = new List<Tour>();
            List<Tour> tours = _tourRepository.FindAll();

            foreach (Tour tour in tours)
            {
                if (IsAMatch(tour, searchTourDto))
                    if (tour.TotalCapacity > searchTourDto.PartySize)
                        eligible.Add(tour);
            }

            return eligible;
        }

        public bool IsAMatch(Tour tour, SearchTourDto searchTourDto)
        {
            bool doesLocationMatch = searchTourDto.Location.Equals(tour.Location) || searchTourDto.Location.IsEmpty();
            bool doesDurationMatch = tour.Duration.Equals(searchTourDto.Duration) || searchTourDto.Duration == 0;
            bool doesLanguageMatch = tour.Language.Equals(searchTourDto.Language) || searchTourDto.Language.Equals("");


            return doesLocationMatch && doesDurationMatch && doesLanguageMatch;
        }



        public SearchTourDto ParseTours(Location location, string language, string duration, string partySize) {

            SearchTourDto searchTourDto = new SearchTourDto();

            searchTourDto.Location = location;
            searchTourDto.Language = language;

            if (duration != String.Empty)
                searchTourDto.Duration = Convert.ToInt32(duration);

            if (partySize != String.Empty)
                searchTourDto.PartySize = Convert.ToInt32(partySize);

            return searchTourDto;
        }

        public List<InstancePackDto> FindUsersTours(Tourist tourist, Status? status) {
            return _tourRepository.FindUsersTours(tourist, status);
        }

        public List<TourInstance> FindTourInstances(Tour tour) {
            return _tourRepository.FindToursInstances(tour.Id);
        }

        public TourInstance FindInstanceByDate(Tour tour, DateTime date)
        {
            return _tourRepository.FindInstanceByDate(date, tour.Id);
        }

        public Checkpoint GetCurrent(int id) {
            return _tourRepository.GetLastCheckpoint(id);
        }

        public List<Checkpoint>GetPrevoius(int id)
        {
            return _tourRepository.GetPreviousCheckpoints(id);
        }

        public List<Checkpoint> GetUpcoming(int id)
        {
            Tour tour = _tourRepository.FindById(id);
            return tour.Checkpoints.FindAll(c => !c.IsChecked);
        }

        public string GetAllDates(Tour tour)
        {
            List<TourInstance> instances = FindTourInstances(tour);

            string dates = String.Empty; 

            foreach (TourInstance instance in instances)
            {
                dates += instance.Date.ToShortDateString() + " | ";
            }

            return dates;

        }

        public List<string> ParseImagePaths(Tour tour)
        {
            List<string> imagepaths = new List<string>();

            foreach(string picture in tour.Pictures)
            {
                imagepaths.Add("/Resources/Images/" + picture);
            }

            return imagepaths;
        }

        public List<string> AllCheckpoints(Tour tour)
        {
            List<string> checkpoints = new List<string>();

            foreach(Checkpoint checkpoint in tour.Checkpoints)
            {
                checkpoints.Add(checkpoint.ToString()+"|");
            }
            
            return checkpoints;
        }

        internal IEnumerable<DateTime> ExtractDates(Tour tour)
        {
            List<TourInstance> instances = FindTourInstances(tour);

            return instances.Select(x => x.Date).ToList();
        }
    }
}
