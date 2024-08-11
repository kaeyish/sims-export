using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using BookingApp.Repository;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.Service
{
    public class SimpleTourRequestService
    {
        private ISimpleTourRequestRepository _repository;

        public SimpleTourRequestService(ISimpleTourRequestRepository repository)
        {
            _repository = repository;
        }

        public void SendNotifications(Tour tour)
        {
            List<SimpleTourRequest>requests = FindAllEveryone();


            var app = (App)Application.Current;
            NotificationService service = app.NotificationService;

            foreach (SimpleTourRequest request in requests)
            {
               if (request.State == State.Pending && (request.Language.Equals(tour.Language) || request.Location.Equals(tour.Location)))
                {
                    Notification notif = new Notification("Kreirana je tura koja ispunjava zahtev id: " + request.RequestId.ToString(), request.UserId, DateTime.Now, tour.Id);
                    service.Save(notif);
                }
            }

        }

        public bool CheckState(List<int> ids) {

            foreach (int id in ids) {
                if (_repository.FindOne(id).State != State.Accepted)
                    return false;
            }

            return true;
        }

        public List<SimpleTourRequest> FindAllEveryone()
        {
            return _repository.FindAllEveryone();
        }
        public List<SimpleTourRequest> FindAll(User user)
        {
            return _repository.FindAll(user);
        }

        public List<string> ParseDates(List<SimpleTourRequest> simpleTourRequests)
        {
            List<string> result = new List<string>();

            foreach (SimpleTourRequest request in simpleTourRequests)
            {
                string temp = request.StartDate + " - " + request.EndDate;
                result.Add(temp);
            }

            return result;

        }

        public SimpleTourRequest Save(User user,string name, string description, string location, string language, DateTime startDate, DateTime endDate, List<Person> people, ReqType type)
        {
            SimpleTourRequest request = new SimpleTourRequest(user.Id, name, description, location.Split(',')[0], location.Split(',')[1], language, people, startDate, endDate, State.Pending, type);
            _repository.Save(request);
            return request;
        }

        public SimpleTourRequest Save(SimpleTourRequest request)
        {
           _repository.Save(request);
            return request;
        }

        public List<KeyValuePair<string, int>> GetLanguageHistogramData(User user, int? year)
        {
            List < KeyValuePair<string, int>> result= new List<KeyValuePair<string, int>>();

            List<SimpleTourRequest> requests = FindAll(user);

            List<string> uniqueLanguages = new List<string>();

            foreach (SimpleTourRequest request in requests)
            {
                if ((year.HasValue ? request.StartDate.Year == year.Value : true) && !uniqueLanguages.Contains(request.Language))
                    uniqueLanguages.Add(request.Language);
            }

            foreach (string language in  uniqueLanguages)
            {
                string key = language;
                int val = requests.Where(u => (year.HasValue ? u.StartDate.Year == year.Value : true) && u.Language == language).ToList().Count();


                result.Add(new KeyValuePair<string, int>(key, val));
            }

            return result;
        }


        public List<KeyValuePair<string, int>> GetLocationHistogramData(User user, int? year)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();

            List<SimpleTourRequest> requests = FindAll(user);

            List<string> uniqueLocations = new List<string>();

            foreach (SimpleTourRequest request in requests)
            {
                if ((year.HasValue ? request.StartDate.Year == year.Value : true) && !uniqueLocations.Contains(request.Location.ToString()))
                    uniqueLocations.Add(request.Location.ToString());
            }

            foreach (string location in uniqueLocations)
            {
                string key = location;
                int val = requests.Where(u => (year.HasValue ? u.StartDate.Year == year.Value : true) && u.Location.ToString() == location).ToList().Count();


                result.Add(new KeyValuePair<string, int>(key, val));
            }

            return result.OrderByDescending(x => x.Value).Take(5).ToList();

        }
        public List<KeyValuePair<string, double>> GetApprovalRate(User user, int? year)
        {
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();

            List<SimpleTourRequest> requests = FindAll(user);

            List<State> states = new List<State>();

            states.Add(State.Pending);
            states.Add(State.Invalid);
            states.Add(State.Accepted);

            int overall = requests.Count();

            foreach (State state in states)
            {
                string key = state.ToString();
                int val = 0;
                foreach (SimpleTourRequest request in requests)
                {
                   

                    if ((year.HasValue ? request.StartDate.Year == year.Value : true) && request.State == state)
                            val++;
                }
                
                double rate = (double)val / overall * 100;


                result.Add(new KeyValuePair<string, double>(key, rate));
            }

            return result;

        }

        internal ObservableCollection<string> GetAllYears(User user)
        {
            List<string> years = new List<string>();

            List<SimpleTourRequest> requests = FindAll(user);

            foreach (SimpleTourRequest request in requests)
            {
                if (!years.Contains(request.StartDate.Year.ToString()))
                    years.Add(request.StartDate.Year.ToString());
            }

            return  new ObservableCollection<string>(years);
        }

        internal string GetAvgPeople(User user, int? year)
        {
            int result = 0;
            int counter = 0;

            List<SimpleTourRequest> requests = FindAll(user);

            foreach (SimpleTourRequest request in requests)
            {
                if ((year.HasValue ? request.StartDate.Year == year.Value : true) && request.State == State.Accepted )
                { 
                    result += request.PartySize;
                    counter++;
                }
            }


            result /= counter;

            return result.ToString();
        }

        internal List<SimpleTourRequest> GetBulk(List<int> ids)
        {
            List<SimpleTourRequest> result = new List<SimpleTourRequest>();


            foreach (int id in ids) {
                result.Add(_repository.FindOne(id));
            }

            return result;

        }

        internal IEnumerable<SimpleTourRequest> Refresh(State state, User user)
        {
            List<SimpleTourRequest> all = FindAll(user);

            return all.Where(x => x.State == state);


        }
    }
}
