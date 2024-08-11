using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.HCI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Service
{
    public class NotificationService
    {
        private INotificationRepository _repository;
        public NotificationService(INotificationRepository notificationRepository) {
            _repository = notificationRepository;
        }

        public List<Notification> FindAll(User user) {
            return _repository.FindAll(user);
        }
        public Notification FindOne(int id) {
            return _repository.FindOne(id);
        }
        public void Save(Notification notification) {
            _repository.Save(notification);
        }

        public bool SendCheckIn(Tour tour, int userID)
        {
            string message = "You've checked in!";
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            _repository.Save(new Notification(message, userID, DateTime.Now,tour.Id));
            
            List<Notification> notifs = _repository.FindAll(userID);

            if (notifs.Where(x => x.Text == message && x.Date >= oneYearAgo).Count() == 5){
                return SendLoyaltyVoucher(userID);
            }
            return false;


        }

        public bool SendLoyaltyVoucher(int id)
        {
            var app = (App)Application.Current;
            VoucherService oneTimeService = app.VoucherService;

            string year = DateTime.Now.Year.ToString();
            string name = "15% off on all tours!";
            string source = year + "'s bonus";

            if(oneTimeService.CheckUnique(id, source)){
                string message = "You've won Loyalty Voucher! See Voucher section for details!";
                _repository.Save(new Notification(message, id, DateTime.Now));

                oneTimeService.Save(id,name,source,DateTime.Now.AddMonths(6));
                     return true;
                }
            return false;
        }




    }
}
