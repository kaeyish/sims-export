using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace BookingApp.Service
{
    public class SuperGuestService
    {
        private ISuperGuestRepository _repository;
        private IUserRepository _userRepository;
        public SuperGuestService(ISuperGuestRepository superGuestRepository, IUserRepository userRepository)
        {
            _repository = superGuestRepository;
            _userRepository = userRepository;
        }

        public void DeleteSuperGuestByGuestId(int guestId)
        {
            _repository.DeleteByGuestId(guestId);
            _userRepository.DemoteFromSuperGuest(guestId);
        }

        public void AddSuperGuest(int guestId)
        {
            SuperGuest temp = new SuperGuest(guestId);
            _repository.Save(temp);
            _userRepository.PromoteToSuperGuest(guestId);
        }

        public bool DeductPoint(int guestId)
        {
            SuperGuest temp = _repository.FindByGuestId(guestId);
            if (temp.DeductPoint())
            {
                Update(temp); 
                return true;
            }
            return false;
        }

        public SuperGuest FindByGuestId(int guestId)
        {
            return _repository.FindByGuestId(guestId);
        }

        public void Save(SuperGuest superGuest)
        {
            _repository.Save(superGuest);
        }

        public bool DeleteWithCondition(int guestId, int reservationsCount)
        {
            if(reservationsCount < 10)
            {
                DeleteSuperGuestByGuestId(guestId);
                return true;
            }

            ResetSuperGuest(guestId);
            return false;
        }

        public bool CheckExpiration(int guestId)
        {
            SuperGuest temp = FindByGuestId(guestId);
            if(temp.EndDate <  DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public void ResetSuperGuest(int guestId)
        {
            SuperGuest temp = FindByGuestId(guestId);
            temp.StartDate = DateTime.Now;
            temp.EndDate = DateTime.Now.AddYears(1);
            temp.Points = 5;
            Update(temp);
        }

        public void Update(SuperGuest superGuest)
        {
            _repository.Update(superGuest);
        }

    }
}
