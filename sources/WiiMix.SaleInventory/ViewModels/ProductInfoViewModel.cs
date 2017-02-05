using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WiiMix.Business.Model;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Interface;
using WiiMix.SaleInventory.Service;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductInfoViewModel : BindableBase, IProductInfoViewModel
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public ProductInfoViewModel(IUnityContainer container, IEventAggregator eventAggregator)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ProductLoadedEvent>().Subscribe(OnLoadedProdcut);

            CancelCommand = new DelegateCommand(OnCancelProduct);
            SaveCommand = new DelegateCommand<Product>(OnSaveProduct);
        }

        private void OnSaveProduct(Product product)
        {
            var isUpdated = product.Id > 0;
            var productService = _container.Resolve<IProductService>();
            product.CategoryId = product.Category.Id;
            product.BrandId = product.Brand.Id;
            if (isUpdated)
            {
                productService.Update(product);
                _eventAggregator.GetEvent<ProductUpdateCompletedEvent>().Publish(product);
            }
            else
            {
                var p = productService.Add(product);
                _eventAggregator.GetEvent<ProductCreateCompletedEvent>().Publish(p);
            }
        }

        private void OnCancelProduct()
        {
            CloseDialog();
        }

        private void OnLoadedProdcut(Product product)
        {
            Title = product == null ? "Create Product" : "Update Product";
            Product = product;
            Initialize();
            if (Product != null)
            {
                if (Product.Category == null)
                {
                    if (Categories.Count > 0) throw new ArgumentNullException("Product.Category", "User creates product without select category.");
                }
                else
                {
                    if (Categories.Count <= 0) throw new ArgumentOutOfRangeException("Product.Category", "There is no category in database.");
                }

                if (Product.Brand == null)
                {
                    if (Brands.Count > 0) throw new ArgumentNullException("Product.Brand", "User creates product without select brand.");
                }
                else
                {
                    if (Brands.Count <= 0) throw new ArgumentOutOfRangeException("Product.Brand", "There is no brand in database.");
                }
                Product.Category = Categories.FirstOrDefault(c => c.Id == Product.CategoryId);
                Product.Brand = Brands.FirstOrDefault(b => b.Id == Product.BrandId);
            }
            else
            {
                Product = new Product
                {
                    Category = Categories.FirstOrDefault(),
                    Brand = Brands.FirstOrDefault(),
                    Config = new Config()
                };
            }
            ShowDialog();
        }

        private void Initialize()
        {
            var categoryService = _container.Resolve<ICategoryService>();
            var brandService = _container.Resolve<IBrandService>();
            if (Categories == null)
            {
                Categories = categoryService.GetAll().ToList();
            }
            if (Brands == null)
            {
                Brands = brandService.GetAll().ToList();
            }
        }

        private Product _product;
        public Product Product
        {
            get { return _product;}
            set {SetProperty(ref _product, value);}
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

        private IList<Brand> _brands;
        public IList<Brand> Brands
        {
            get { return _brands; }
            set { SetProperty(ref _brands, value); }
        }
       
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

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