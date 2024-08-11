using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IComplexRequestRepository
    {
        List<ComplexTourRequest> FindAll(User user);
        List<ComplexTourRequest> FindAllEveryone();
        ComplexTourRequest? FindOne(int id);
        void Save(ComplexTourRequest request);
    }
}