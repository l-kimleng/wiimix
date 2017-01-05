using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WiiMix.Data;
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

        private void Initialize()
        {
            using (var unitOfWork = UnitOfWork)
            {
                if (Categories == null)
                    Categories = unitOfWork.CategoryRepository.GetAll().ToList();
                if (Brands == null)
                    Brands = unitOfWork.BrandRepository.GetAll().ToList();
            }

            var category = Categories.FirstOrDefault(c => c.Id == Product.CategoryId);
            if (category == null)
            {
                if (Categories.Count > 0) SelectedCategory = Categories[0];
            }
            else
            {
                SelectedCategory = category;
            }

            var brand = Brands.FirstOrDefault(b => b.Id == Product.BrandId);
            if (brand == null)
            {
                if (Brands.Count > 0) SelectedBrand = Brands[0];
            }
            else
            {
                SelectedBrand = brand;
            }
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

        private IList<Category> _categories;
        public IList<Category> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }


        private IList<Brand> _brands;
        public IList<Brand> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }

        private Brand _selectedBrand;
        public Brand SelectedBrand
        {
            get { return _selectedBrand; }
            set { SetProperty(ref _selectedBrand, value); }
        }

        [Microsoft.Practices.Unity.Dependency]
        public IUnitOfWork UnitOfWork { get; set; }

        public ICommand CancelCommand { get; private set; }

        private IProductInfoView _productInfoView;

        public void ShowDialog()
        {
            Initialize();
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
