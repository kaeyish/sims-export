using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Service
{
    public class ComplexRequestService
    {
        private IComplexRequestRepository _repository;
        public ComplexRequestService(IComplexRequestRepository repository) {
            _repository = repository;
        }

        public List<ComplexTourRequest> FindAll(User user) {
            return _repository.FindAll(user);
        }

        public List<ComplexTourRequest> FindAllEveryone() {
            return _repository.FindAllEveryone();
        }

        public ComplexTourRequest FindOne(User user) {
            return _repository.FindOne(user.Id);
        }
        public void Save(User user, string name, string description,List<SimpleTourRequest> requests)
        {
            List<int> ids = new List<int>();

            SimpleTourRequest temp = requests[0];


            foreach (SimpleTourRequest tourRequest in requests) {
                if (tourRequest.StartDate < temp.StartDate)
                    temp = tourRequest;
            }

            foreach (SimpleTourRequest request in requests)
            {
                ids.Add(request.RequestId);
            }


            ComplexTourRequest complexRequest = new ComplexTourRequest(name, description, temp.StartDate, ids, user.Id);
            _repository.Save(complexRequest);
        }

        internal IEnumerable<ComplexTourRequest> Refresh(State state, User user)
        {
            List<ComplexTourRequest> all = FindAll(user);

            return all.Where(x => x.Status == state);

        }

    }
}
