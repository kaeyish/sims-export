using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVoucherRepository
    {
        public void Delete(Voucher activeVoucher);

        public List<Voucher> FindAll();

        public List<Voucher> FindAllByUser(int id);

        public Voucher FindOne(int id);

        public void Save(Voucher voucher);

        public void Update(Voucher activeVoucher);
    }
}
