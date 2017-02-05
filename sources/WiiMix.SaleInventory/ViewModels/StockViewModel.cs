using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WiiMix.Business.Model;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Interface;
using WiiMix.SaleInventory.Service;

namespace WiiMix.SaleInventory.ViewModels
{
    public class StockViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IStockService _stockService;

        public StockViewModel(IEventAggregator eventAggregator, IStockService stockService)
        {
            _eventAggregator = eventAggregator;
            _stockService = stockService;
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
            Stocks = new ObservableCollection<Stock>(_stockService.FindAllDetail());
            StockCollectionView = CollectionViewSource.GetDefaultView(Stocks);
            if (Stocks.Count > 0)
            {
                SelectedStock = Stocks[0];
            }
        }

        [Microsoft.Practices.Unity.Dependency]
        public IStockInfoViewModel StockInfoViewModel { get; set; }
        public ICommand AddStockCommand { get; private set; }
        public ICommand UpdateStockCommand { get; private set; }
    }
}
