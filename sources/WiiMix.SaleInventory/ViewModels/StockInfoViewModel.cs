using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
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
            eventAggregator.GetEvent<StockLoadedEvent>().Subscribe(OnLoadedStock);
        }

        private void OnProductAddedToCart(Product product)
        {
            if(Stock.Details == null) Stock.Details = new ObservableCollection<StockDetail>();
            Stock.Details.Add(new StockDetail
            {
                ProductId = product.Id,
                Quantity = 1,
                Price = product.Config.Price,
                Product = product,
            });
            Stock.Quantity++;
            Stock.TotalPrice += product.Config.Price;
            Products.RemoveAt(Products.IndexOf(Products.FirstOrDefault(x => x.Id == product.Id)));
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
            get { return _stock ?? (_stock = new Stock()); }
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
    }
}
