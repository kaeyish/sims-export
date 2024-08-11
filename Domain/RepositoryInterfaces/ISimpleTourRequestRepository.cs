using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Repository
{
    public interface ISimpleTourRequestRepository
    {
        List<SimpleTourRequest> FindAll(User user, ReqType? type=null);
        List<SimpleTourRequest> FindAllEveryone();
        SimpleTourRequest? FindOne(int id);
        void Save(SimpleTourRequest request);
        void Update();
    }
}