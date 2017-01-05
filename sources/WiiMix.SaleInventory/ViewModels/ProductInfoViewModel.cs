using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;
using WiiMix.Data.Entities;
using WiiMix.SaleInventory.Events;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductInfoViewModel : BindableBase, IProductInfoViewModel
    {
        private readonly IUnityContainer _container;
        public ProductInfoViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            eventAggregator.GetEvent<ProductUpdatedEvent>().Subscribe(OnUpdatedProdcut);
            CancelCommand = new DelegateCommand(OnCancelProduct);
        }

        private void OnCancelProduct()
        {
            CloseDialog();
        }

        private void OnUpdatedProdcut(Product updateProduct)
        {
            Product = updateProduct;
            Title = "Update Product";
        }

        private Product _product;
        public Product Product
        {
            get { return _product;}
            set { SetProperty(ref _product, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ICommand CancelCommand { get; private set; }

        private IProductInfoView _productInfoView;

        public void ShowDialog()
        {
            _productInfoView = _container.Resolve<IProductInfoView>();
            _productInfoView.DataContext = this;
            _productInfoView.ShowPopup();
        }

        public void CloseDialog()
        {
            _productInfoView?.ClosePopup();
        }
    }
}
