using Prism.Events;
using Prism.Mvvm;
using WiiMix.Data.Entities;
using WiiMix.SaleInventory.Events;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductInfoViewModel : BindableBase
    {
        public ProductInfoViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ProductUpdatedEvent>().Subscribe(OnUpdatedProdcut);
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

    }
}
