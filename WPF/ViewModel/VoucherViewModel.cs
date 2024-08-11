using BookingApp.Domain.Model;
using BookingApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModel
{
    public class VoucherViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Voucher> _vouchers;

        private VoucherService _service;

        public ObservableCollection<Voucher> Vouchers
        {
            get { return _vouchers; }
            set { _vouchers = value; OnPropertyChanged("Vouchers"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private Frame _frame;

      
        public VoucherViewModel(User user, Frame frame)
        {
            var app = (App)Application.Current;
            _service = app.VoucherService;
            Vouchers = new ObservableCollection<Voucher>(_service.FindUsersVouchers(user));
            _frame = frame;
        }

        public void ApplyVoucher(Voucher voucher, User user, Tour tour)
        {
            Vouchers = new ObservableCollection<Voucher>(_service.ApplyVoucher(voucher, user, tour));
        }


        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(v));
        }

    }
}
