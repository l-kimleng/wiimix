using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WiiMix.Business.Model;
using WiiMix.Data;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Interface;
using WiiMix.SaleInventory.Service;

namespace WiiMix.SaleInventory.ViewModels
{
    public class StockInfoViewModel : BindableBase, IStockInfoViewModel
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private IUnitOfWork _unitOfWork;
        private IStockInfoView _stockInfoView;

        public StockInfoViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            CancelCommand = new DelegateCommand(OnCancelCommand);
            AddToCartCommand = new DelegateCommand<Product>(OnProductAddedToCart);
            RemoveFromCartCommand = new DelegateCommand<StockDetail>(OnProductRemoveFromCartCommand);
            SaveStockCommand = new DelegateCommand<Stock>(OnStockSave);
            eventAggregator.GetEvent<StockLoadedEvent>().Subscribe(OnLoadedStock);
        }

        private void OnStockSave(Stock stock)
        {
            var isUpdated = stock.Id > 0;
            var stockService = _container.Resolve<IStockService>();
            if (isUpdated)
            {
                if (stockService.Update(Stock) > 0)
                {
                    _eventAggregator.GetEvent<StockUpdateCompletedEvent>().Publish(Stock);
                }
            }
            else
            {
                var newStock = stockService.Add(stock);
                Stock.Id = newStock.Id;
                foreach (var stockDetail in Stock.Details)
                {
                    var s = newStock.Details.SingleOrDefault(x => x.ProductId == stockDetail.ProductId);
                    if (s == null) continue;
                    stockDetail.Id = s.Id;
                    stockDetail.ProductId = s.ProductId;
                    stockDetail.StockId = s.StockId;
                }
                _eventAggregator.GetEvent<StockCreateCompletedEvent>().Publish(Stock);
            }
        }

        private void OnProductRemoveFromCartCommand(StockDetail stockDetail)
        {
            var details = Stock.Details;
            details.RemoveAt(details.IndexOf(details.FirstOrDefault(x => x.ProductId == stockDetail.ProductId)));
            Products.Add(stockDetail.Product);
            Stock.Quantity -= stockDetail.Quantity;
            Stock.TotalPrice -= (decimal)stockDetail.Quantity*stockDetail.Price;
        }

        private void OnProductAddedToCart(Product product)
        {
            var productItem = new StockDetail
            {
                ProductId = product.Id,
                Quantity = 1,
                Price = product.Config.Price,
                Product = product
            };
            Add(productItem);
        }

        private void Add(StockDetail stockDetail)
        {
            if (Stock.Details == null) Stock.Details = new ObservableCollection<StockDetail>();

            stockDetail.PropertyChanged += ProductItem_PropertyChanged;
            Stock.Details.Add(stockDetail);
            Stock.Quantity++;
            Stock.TotalPrice += stockDetail.Price;
            Products.RemoveAt(Products.IndexOf(Products.FirstOrDefault(x => x.Id == stockDetail.ProductId)));
        }

        private void ProductItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Quantity" || e.PropertyName == "Price")
            {
                var stockDetail = sender as StockDetail;
                if (stockDetail != null)
                {
                    Stock.Quantity = 0;
                    Stock.TotalPrice = 0;
                    foreach (var product in Stock.Details)
                    {
                        Stock.Quantity += product.Quantity;
                        Stock.TotalPrice += (decimal)product.Quantity*product.Price;
                    }
                }
            }
        }

        private void OnCancelCommand()
        {
            _stockInfoView.ClosePopup();
            _stockInfoView = null;
        }

        private void OnLoadedStock(Stock stock)
        {
            Stock = new Stock { Date = DateTime.Now };
            Initialize();
            if (stock != null)
            {
                Stock.Id = stock.Id;
                Stock.Date = stock.Date;
                Stock.Quantity = stock.Quantity;
                Stock.TotalPrice = stock.TotalPrice;
                foreach (var stockDetail in stock.Details)
                {
                    Add(stockDetail);
                }
            }
            ShowDialog();
        }

        private void Initialize()
        {
            using (_unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var products = _unitOfWork.Products.Find();
                Products = new ObservableCollection<Product>();
                foreach (var product in products)
                {
                    Products.Add(new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CategoryId = product.CategoryId,
                        BrandId = product.BrandId,
                        Category = new Category { Id = product.CategoryId, Name = product.Category.Name},
                        Brand = new Brand { Id = product.BrandId, Name = product.Brand.Name},
                        Config = new Config { ProductId = product.Id, Feature = product.Config.Feature, Price = product.Config.Price}
                    });
                }
                ProductCollectionView = CollectionViewSource.GetDefaultView(Products);

                if (Products.Any())
                {
                    SelectedProduct = Products.FirstOrDefault();
                }
            }
        }

        private void ShowDialog()
        {
            _stockInfoView = _container.Resolve<IStockInfoView>();
            _stockInfoView.DataContext = this;
            _stockInfoView.ShowPopup();
        }

        private Stock _stock;
        public Stock Stock
        {
            get { return _stock; }
            set { SetProperty(ref _stock, value); }
        }

        private ICollectionView _productCollectionView;
        public ICollectionView ProductCollectionView
        {
            get { return _productCollectionView;}
            set { SetProperty(ref _productCollectionView, value); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products   
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }


        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct;}
            set { SetProperty(ref _selectedProduct, value); }
        }

        public ICommand CancelCommand { get; private set; }
        public ICommand AddToCartCommand { get; private set; }
        public ICommand RemoveFromCartCommand { get; private set; }
        public ICommand SaveStockCommand { get; private set; }
    }
}
