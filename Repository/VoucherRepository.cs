using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private List<Voucher> _vouchers;

        private List<Voucher> _vouchersByUser;

        private readonly Serializer<Voucher> _serializer;

        public VoucherRepository() {

            _serializer = new Serializer<Voucher>();

            _vouchers = ClearUp(_serializer.FromCSV(FilePath));

            _vouchersByUser = new List<Voucher>();

        }

        public List<Voucher> ClearUp(List<Voucher> vouchers) {
            List<Voucher> temps = new List<Voucher>();
            DateOnly now = DateOnly.FromDateTime(DateTime.Today);

            foreach (Voucher voucher in vouchers)
            {
                if (voucher.ExpiryDate >= now)
                    temps.Add(voucher);
            }

            _serializer.ToCSV(FilePath, temps);

            return temps;

        }

        public void Delete(Voucher activeVoucher)
        {
            _vouchers.Remove(activeVoucher);
            _serializer.ToCSV(FilePath, _vouchers);
        }


        public List<Voucher> FindAll()
        {
            return _vouchers;
        }

        public List <Voucher> FindAllByUser(int id)
        {
            List<Voucher> vouchers = new List<Voucher>();

            foreach (Voucher voucher in _vouchers)
            {
                if (voucher.TouristId == id)
                 vouchers.Add(voucher); 
            }

            _vouchersByUser = vouchers;
            return _vouchersByUser;
        }

        public Voucher FindOne(int id) {
            foreach (Voucher voucher in _vouchersByUser)
                if (voucher.Id == id)
                    return voucher;
            return null;
        }

        public void Save(Voucher voucher) {
            voucher.Id = NextId();
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
        }

        public void Update(Voucher activeVoucher) {
            List<Voucher> tempVouchers = new List<Voucher>();

            foreach(Voucher voucher in _vouchers)
            {
                if (voucher.Id == activeVoucher.Id)
                {
                    tempVouchers.Add(activeVoucher);
                }
                else tempVouchers.Add(voucher);
            }
            _serializer.ToCSV(FilePath, tempVouchers);
            _vouchers = tempVouchers;
        }

        private int NextId()
        {
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(t => t.Id) + 1;
        }


    }
}
