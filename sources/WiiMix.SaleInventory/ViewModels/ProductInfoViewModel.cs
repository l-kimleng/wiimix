using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WiiMix.Data;
using WiiMix.Data.Entities;
using WiiMix.SaleInventory.Events;
using DelegateCommand = Prism.Commands.DelegateCommand;

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
            SaveCommand = new DelegateCommand<Product>(OnSaveProduct);
        }

        private void OnSaveProduct(Product product)
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var savedProduct = unitOfWork.ProductRepository.Get(product.Id);
                savedProduct.Name = product.Name;
                savedProduct.CategoryId = product.Category.Id;
                savedProduct.BrandId = product.Brand.Id;
                if (product.Config.ProductId <= 0) // Add new config
                {
                    savedProduct.Config = new Config
                    {
                        ProductId = product.Id,
                        Feature = product.Config.Feature,
                        Price = product.Config.Price,
                        Image = product.Config.Image
                    };
                }
                else // user just need to update the existing config
                {
                    var updatedConfig = unitOfWork.ConfigRepository.Get(product.Id);
                    updatedConfig.Feature = product.Config.Feature;
                    updatedConfig.Price = product.Config.Price;
                    updatedConfig.Image = product.Config.Image;
                    unitOfWork.ConfigRepository.Update(updatedConfig);
                }
                
                unitOfWork.ProductRepository.Update(savedProduct);
                unitOfWork.Completed();
            }
        }

        private void OnCancelProduct()
        {
            CloseDialog();
        }

        private void OnUpdatedProdcut(Product updateProduct)
        {
            Title = "Update Product";
            Product = updateProduct;
            Initialize();
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

        private void Initialize()
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                if (Categories == null)
                    Categories = unitOfWork.CategoryRepository.GetAll().ToList();
                if (Brands == null)
                    Brands = unitOfWork.BrandRepository.GetAll().ToList();
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
