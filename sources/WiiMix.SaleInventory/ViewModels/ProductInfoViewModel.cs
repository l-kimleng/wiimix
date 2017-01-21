using AutoMapper;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WiiMix.Data;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Interface;
using WiiMix.SaleInventory.Models;

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
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var savedProduct = isUpdated ? unitOfWork.ProductRepository.FindUpdate(product.Id) : new Data.Entities.Product {Config = new Data.Entities.Config()};
                savedProduct.Name = product.Name;
                savedProduct.CategoryId = product.Category.Id;
                savedProduct.BrandId = product.Brand.Id;

                savedProduct.Config.Feature = product.Config.Feature;
                savedProduct.Config.Price = product.Config.Price;
                savedProduct.Config.Image = product.Config.Image;
                var newProductId = 0;
                var result = 0;
                if (isUpdated)
                {
                    savedProduct.Config.ProductId = product.Id;
                    unitOfWork.ProductRepository.Update(savedProduct);
                    result = unitOfWork.Completed();
                }
                else
                {
                    var p = unitOfWork.ProductRepository.Add(savedProduct);
                    result = unitOfWork.Completed();
                    newProductId = p.Id;
                }
                if (result > 0)
                {
                    if (newProductId > 0)
                    {
                        product.Id = newProductId;
                        product.CategoryId = product.Category.Id;
                        product.BrandId = product.Brand.Id;
                        product.Config.ProductId = newProductId;
                        _eventAggregator.GetEvent<ProductCreateCompletedEvent>().Publish(product);
                    }
                    else
                    {
                        _eventAggregator.GetEvent<ProductUpdateCompletedEvent>().Publish(product);
                    }
                }
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
                    Category = Categories.Take(1).FirstOrDefault(),
                    Brand = Brands.Take(1).FirstOrDefault(),
                    Config = new Config()
                };
            }
            ShowDialog();
        }

        private void Initialize()
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                if (Categories == null)
                {
                    Categories = new List<Category>();
                    foreach (var category in unitOfWork.CategoryRepository.GetAll())
                    {
                        Categories.Add(Mapper.Map<Category>(category));
                    }
                }
                if (Brands == null)
                {
                    Brands = new List<Brand>();
                    foreach (var brand in unitOfWork.BrandRepository.GetAll())
                    {
                        Brands.Add(Mapper.Map<Brand>(brand));
                    }
                }
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