using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WiiMix.Data;
using WiiMix.Data.Entities;
using WiiMix.SaleInventory.Events;

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
        }

        private void OnClickUpdatedCommand(Product product)
        {
            _eventAggregator.GetEvent<ProductUpdatedEvent>().Publish(product);
            ProductInfoViewModel.ShowDialog();
        }

        [Microsoft.Practices.Unity.Dependency]
        public IProductInfoViewModel ProductInfoViewModel { get; set; }

        private void GetAll()
        {
            using (var unitOfwork = _unitOfWork)
            {
                var productRepository = unitOfwork.ProductRepository;
                var products = productRepository.Display().ToList();
                Products = CollectionViewSource.GetDefaultView(products);
                if (products.Count > 0)
                {
                    SelectedProduct = products[0];
                }
            }
        }

        private ICollectionView _products;
        public ICollectionView Products
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
    }
}
