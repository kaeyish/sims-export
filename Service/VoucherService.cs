using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Service
{
    public class VoucherService
    {
        private IVoucherRepository _repository;

        public VoucherService(IVoucherRepository voucherRepository) {
            _repository = voucherRepository;
        }

        public List<Voucher> FindAllVouchers() {
            return _repository.FindAll();
        }

        public List<Voucher> FindUsersVouchers(User user) {
            return _repository.FindAllByUser(user.Id);
        } 

        public Voucher FindVoucher(int id) {
            return _repository.FindOne(id);
        }

        public List<Voucher> ApplyVoucher (Voucher voucher, User user, Tour tour)
        {
            //ako je vaucer samo za ture tog vodica, nece ga iskoristiti
            if(voucher.GuideId.HasValue)
                if (voucher.GuideId.Value != tour.GuideId)
                    return FindUsersVouchers(user);

            _repository.Delete(voucher);
            return FindUsersVouchers(user);
        }

        public void Save(int touristId, string name, string source, DateTime expiryDate) {
            Voucher voucher = new Voucher();

            voucher.TouristId = touristId;
            voucher.Name = name;
            voucher.Source = source;
            voucher.ExpiryDate = DateOnly.FromDateTime(expiryDate);

            _repository.Save(voucher);

        }

        internal bool CheckUnique(int id, string source)
        {
            foreach (Voucher voucher in _repository.FindAllByUser(id)) {
                if (voucher.Source == source)
                    return false;
            }
            return true;
        }
    }
}
