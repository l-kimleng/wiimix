using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WiiMix.Data;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnitOfWork _unitOfWork;

        public ProductViewModel(IEventAggregator eventAggregator, IUnitOfWork unitOfWork)
        {
            _eventAggregator = eventAggregator;
            _unitOfWork = unitOfWork;
            GetAll();
            UpdateCommand = new DelegateCommand<Product>(OnClickUpdatedCommand);
            AddNewCommand = new DelegateCommand(OnAddNewProductCommand);
        }

        private void OnAddNewProductCommand()
        {
            _eventAggregator.GetEvent<ProductLoadedEvent>().Publish(null);
        }

        private void OnClickUpdatedCommand(Product selectedProduct)
        {
            var product = selectedProduct.Clone();
            _eventAggregator.GetEvent<ProductUpdateCompletedEvent>().Subscribe(OnUpdateProductCompleted);
            _eventAggregator.GetEvent<ProductLoadedEvent>().Publish(product);
        }

        private void OnUpdateProductCompleted(Product product)
        {
            SelectedProduct.Name = product.Name;
            SelectedProduct.BrandId = product.BrandId;
            SelectedProduct.CategoryId = product.CategoryId;
            SelectedProduct.Category.Id = product.CategoryId;
            SelectedProduct.Category.Name = product.Category.Name;
            SelectedProduct.Brand.Id = product.BrandId;
            SelectedProduct.Brand.Name = product.Brand.Name;
            SelectedProduct.Config.ProductId = product.Config.ProductId;
            SelectedProduct.Config.Feature = product.Config.Feature;
            SelectedProduct.Config.Image = product.Config.Image;
            SelectedProduct.Config.Price = product.Config.Price;
        }

        [Microsoft.Practices.Unity.Dependency]
        public IProductInfoViewModel ProductInfoViewModel { get; set; }

        private void GetAll()
        {
            using (var unitOfwork = _unitOfWork)
            {
                var productRepository = unitOfwork.ProductRepository;
                Products = new ObservableCollection<Product>();
                //Products.AddRange(Mapper.Map<IEnumerable<Product>>(productRepository.Display()));
                foreach (var product in productRepository.Display())
                {
                    Products.Add(new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        CategoryId = product.CategoryId,
                        BrandId = product.BrandId,
                        Category = new Category
                        {
                            Id = product.Category.Id,
                            Name = product.Category.Name
                        },
                        Brand = new Brand
                        {
                            Id = product.Brand.Id,
                            Name = product.Brand.Name
                        },
                        Config = new Config
                        {
                            ProductId = product.Config.ProductId,
                            Feature = product.Config.Feature,
                            Price = product.Config.Price,
                            Image = product.Config.Image
                        }
                    });
                    //Products.Add(Mapper.Map<Product>(product));
                }
                ProductCollectionView = CollectionViewSource.GetDefaultView(Products);
                if (Products.Count > 0)
                {
                    SelectedProduct = Products[0];
                }
            }
        }

        private ICollectionView _productCollectionView;
        public ICollectionView ProductCollectionView
        {
            get { return _productCollectionView; }
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
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        public ICommand UpdateCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
    }
}
