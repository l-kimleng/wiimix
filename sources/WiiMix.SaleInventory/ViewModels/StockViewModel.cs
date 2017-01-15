using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WiiMix.Data;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.ViewModels
{
    public class StockViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnitOfWork _unitOfWork;

        public StockViewModel(IEventAggregator eventAggregator, IUnitOfWork unitOfWork)
        {
            _eventAggregator = eventAggregator;
            _unitOfWork = unitOfWork;
            GetAll();
            AddStockCommand = new DelegateCommand(OnStockAddCommand);
            UpdateStockCommand = new DelegateCommand<Stock>(OnStockUpdateCommand);
            eventAggregator.GetEvent<StockCreateCompletedEvent>().Subscribe(OnSaveStockCompleted);
            eventAggregator.GetEvent<StockUpdateCompletedEvent>().Subscribe(OnSaveStockCompleted);
        }

        private void OnStockUpdateCommand(Stock stock)
        {
            _isUpdated = true;
            _eventAggregator.GetEvent<StockLoadedEvent>().Publish(stock);
        }

        private bool _isUpdated = true;
        private void OnSaveStockCompleted(Stock stock)
        {
            if (_isUpdated)
            {
                SelectedStock.Date = stock.Date;
                SelectedStock.Quantity = stock.Quantity;
                SelectedStock.TotalPrice = stock.TotalPrice;
                foreach (var stockDetail in SelectedStock.Details)
                {
                    var d = stock.Details.FirstOrDefault(x => x.ProductId == stockDetail.ProductId);
                    if (d == null) continue;
                    stockDetail.Quantity = d.Quantity;
                    stockDetail.Price = d.Price;
                }
            }
            else
            {
                Stocks.Add(stock);
            }
        }

        private void OnStockAddCommand()
        {
            _isUpdated = false;
            _eventAggregator.GetEvent<StockLoadedEvent>().Publish(null);
        }

        private ObservableCollection<Stock> _stocks;
        public ObservableCollection<Stock> Stocks
        {
            get { return _stocks;}
            set { SetProperty(ref _stocks, value); }
        }

        private ICollectionView _stockCollectionView;
        public ICollectionView StockCollectionView
        {
            get { return _stockCollectionView;}
            set { SetProperty(ref _stockCollectionView, value); }
        }

        private Stock _selectedStock;
        public Stock SelectedStock
        {
            get { return _selectedStock;}
            set { SetProperty(ref _selectedStock, value); }
        }

        private void GetAll()
        {
            using (var unitOfWork = _unitOfWork)
            {
                var stocks = unitOfWork.StockRepository.FindAllDetail();
                Stocks = new ObservableCollection<Stock>();
                foreach (var stock in stocks)
                {
                    Stocks.Add(BuildStock(stock));
                }
            }

            StockCollectionView = CollectionViewSource.GetDefaultView(Stocks);
            if (Stocks.Count > 0)
            {
                SelectedStock = Stocks[0];
            }
        }

        private Stock BuildStock(WiiMix.Data.Entities.Stock stock)
        {
            var myStock = new Stock
            {
                Id = stock.Id,
                Date = stock.Date,
                Quantity = stock.Quantity,
                TotalPrice = stock.TotalPrice,
                Details = new ObservableCollection<StockDetail>()
            };
            foreach (var detail in stock.Details)
            {
                myStock.Details.Add(BuildStockDetail(detail));
            }

            return myStock;
        }

        [Microsoft.Practices.Unity.Dependency]
        public IStockInfoViewModel StockInfoViewModel { get; set; }
        public ICommand AddStockCommand { get; private set; }
        public ICommand UpdateStockCommand { get; private set; }
        private IList<Product> _products;
        private StockDetail BuildStockDetail(WiiMix.Data.Entities.StockDetail detail)
        {
            var myDetail = new StockDetail();
            using (_unitOfWork)
            {
                if (_products == null)
                {
                    _products = new List<Product>();
                    foreach (var product in _unitOfWork.ProductRepository.Find())
                    {
                        _products.Add(AutoMapper.Mapper.Map<Product>(product));
                    }
                }
                var p = _products.SingleOrDefault(x => x.Id == detail.ProductId);
                if (p != null)
                {
                    myDetail.Id = detail.Id;
                    myDetail.ProductId = p.Id;
                    myDetail.Quantity = detail.Quantity;
                    myDetail.Price = detail.Price;
                    myDetail.StockId = detail.StockId;
                    myDetail.Product = p;

                }
            }
            return myDetail;
        }
    }
}
