using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace WiiMix.SaleInventory.Models
{
    public class Stock : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private float _quantity;
        public float Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { SetProperty(ref _totalPrice, value); }
        }

        private ObservableCollection<StockDetail> _details;
        public ObservableCollection<StockDetail> Details
        {
            get { return _details; }
            set { SetProperty(ref _details, value); }
        }
    }
}