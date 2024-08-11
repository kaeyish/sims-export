using BookingApp.Domain.Model;
using BookingApp.Serializer;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Repository
{
    class SimpleTourRequestRepository : ISimpleTourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequests.csv";

        private readonly Serializer<SimpleTourRequest> _serializer;

        private List<SimpleTourRequest> _requests;

        public SimpleTourRequestRepository()
        {
            _serializer = new Serializer<SimpleTourRequest>();
            _requests = _serializer.FromCSV(FilePath);
            Update();
        }

        public void Save(SimpleTourRequest request)
        {
            if (request.RequestId == 0)
            {
                request.RequestId = NextId();
                _requests = _serializer.FromCSV(FilePath);
                _requests.Add(request);
            }
            else
            {
                List<SimpleTourRequest> temps = new List<SimpleTourRequest>();
                foreach (SimpleTourRequest rq in _requests)
                {
                    if (rq.RequestId == request.RequestId)
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
            List<SimpleTourRequest> temps = _requests;

            foreach (SimpleTourRequest request in _requests)
            {
                TimeSpan timeDifference = request.StartDate - DateTime.Now;
                if (request.State == State.Pending && timeDifference.TotalHours < 48){
                    request.State = State.Invalid;
                }
            }
            _requests = temps;
            _serializer.ToCSV(FilePath, _requests);
        }

        public List<SimpleTourRequest> FindAll(User user, ReqType? type = null)
        {
            List<SimpleTourRequest> result = new List<SimpleTourRequest>();

            if (type == null)
                result.AddRange(_requests.Where(u => u.UserId == user.Id && u.Type == ReqType.Simple).ToList());
            else result.AddRange(_requests.Where(u => u.UserId == user.Id && u.Type == ReqType.Complex).ToList());
            return result;
        }

        public SimpleTourRequest? FindOne(int id)
        {
            return _requests.FirstOrDefault(u => u.RequestId == id);
        }


        public int NextId()
        {
            _requests = _serializer.FromCSV(FilePath);
            if (_requests.Count < 1)
            {
                return 1;
            }
            return _requests.Max(a => a.RequestId) + 1;
        }

        public List<SimpleTourRequest> FindAllEveryone()
        {
            return _requests;
        }
    }
}
