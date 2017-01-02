using Prism.Mvvm;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using WiiMix.Data;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            GetAll();
        }

        private void GetAll()
        {
            using (var unitOfwork = _unitOfWork)
            {
                var productRepository = unitOfwork.ProductRepository;
                var products = productRepository.Display().Select(x => new
                {
                    x.Id,
                    x.Name,
                    CategoryName = x.Category.Name,
                    BrandName = x.Brand.Name,
                    x.Config.Feature,
                    x.Config.Price,
                    x.Config.Image
                });
                Products = CollectionViewSource.GetDefaultView(products);
            }
        }

        private ICollectionView _products;

        public ICollectionView Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }
    }
}
