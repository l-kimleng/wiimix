using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
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
    public class StockInfoViewModel : BindableBase, IStockInfoViewModel
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;
        private IStockInfoView _stockInfoView;

        public StockInfoViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            CancelCommand = new DelegateCommand(OnCancelCommand);
            AddToCartCommand = new DelegateCommand<Product>(OnProductAddedToCart);
            RemoveFromCartCommand = new DelegateCommand<StockDetail>(OnProductRemoveFromCartCommand);
            SaveStockCommand = new DelegateCommand<Stock>(OnStockSave);
            eventAggregator.GetEvent<StockLoadedEvent>().Subscribe(OnLoadedStock);
        }

        private void OnStockSave(Stock stock)
        {
            using (_unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var saveStock = new Data.Entities.Stock();
                saveStock.Date = stock.Date;
                saveStock.Quantity = stock.Quantity;
                saveStock.TotalPrice = stock.TotalPrice;
                saveStock.Details = new List<Data.Entities.StockDetail>();
                foreach (var detail in stock.Details)
                {
                    saveStock.Details.Add(new Data.Entities.StockDetail
                    {
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        Price = detail.Price,
                    });
                }
                _unitOfWork.StockRepository.Add(saveStock);
                var result = _unitOfWork.Completed();
                if (result > 0)
                {
                    
                }
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
            if(Stock.Details == null) Stock.Details = new ObservableCollection<StockDetail>();
            var productItem = new StockDetail
            {
                ProductId = product.Id,
                Quantity = 1,
                Price = product.Config.Price,
                Product = product
            };
            productItem.PropertyChanged += ProductItem_PropertyChanged;
            Stock.Details.Add(productItem);
            Stock.Quantity++;
            Stock.TotalPrice += product.Config.Price;
            Products.RemoveAt(Products.IndexOf(Products.FirstOrDefault(x => x.Id == product.Id)));
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
            Initialize();
            ShowDialog();
        }

        private void Initialize()
        {
            using (_unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var products = _unitOfWork.ProductRepository.Find();
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
            get { return _stock ?? (_stock = new Stock() {Date = DateTime.Now}); }
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
