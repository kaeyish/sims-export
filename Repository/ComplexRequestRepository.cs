using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Repository
{
    public class ComplexRequestRepository : IComplexRequestRepository
    {

        private const string FilePath = "../../../Resources/Data/complexRequests.csv";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _requests;

        public ComplexRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _requests = _serializer.FromCSV(FilePath);
            Update();
        }

        public void Save(ComplexTourRequest request)
        {
            if (request.Id == 0)
            {
                request.Id = NextId();
                _requests = _serializer.FromCSV(FilePath);
                _requests.Add(request);
            }
            else
            {
                List<ComplexTourRequest> temps = new List<ComplexTourRequest>();
                foreach (ComplexTourRequest rq in _requests)
                {
                    if (rq.Id == request.Id)
                        temps.Add(request);
                    else temps.Add(rq);
                }
                _requests = temps;
            }
            Update();
            _serializer.ToCSV(FilePath, _requests);
        }

        public void Update()
        {
            var app = (App)Application.Current;
            SimpleTourRequestService tempService = app.SimpleTourRequestService;

            List<ComplexTourRequest> temps = new List<ComplexTourRequest>();


            foreach (ComplexTourRequest request in _requests)
            {
                TimeSpan timeDifference = request.StartDate - DateTime.Now;
                if(request.Status == State.Pending){
                    if (tempService.CheckState(request.Segments))
                        request.Status = State.Accepted;
                    if (timeDifference.TotalHours < 48)
                        request.Status = State.Invalid;
                }

                temps.Add(request);
            }

            _requests = temps;
            _serializer.ToCSV(FilePath, _requests);
        }

        public List<ComplexTourRequest> FindAll(User user)
        {
            List<ComplexTourRequest> result = new List<ComplexTourRequest>();
            result.AddRange(_requests.Where(u => u.UserId == user.Id).ToList());
            return result;
        }

        public ComplexTourRequest? FindOne(int id)
        {
            return _requests.FirstOrDefault(u => u.Id == id);
        }


        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(a => a.Id) + 1;
        }

        public List<ComplexTourRequest> FindAllEveryone()
        {
            return _requests;
        }
    }

}

