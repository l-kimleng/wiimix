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
using Brand = WiiMix.SaleInventory.Models.Brand;
using Category = WiiMix.SaleInventory.Models.Category;
using Product = WiiMix.SaleInventory.Models.Product;

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
                var savedProduct = isUpdated ? unitOfWork.ProductRepository.FindUpdate(product.Id) : new Data.Entities.Product { Config = new Config() };
                savedProduct.Name = product.Name;
                savedProduct.CategoryId = product.Category.Id;
                savedProduct.BrandId = product.Brand.Id;

                savedProduct.Config.Feature = product.Config.Feature;
                savedProduct.Config.Price = product.Config.Price;
                savedProduct.Config.Image = product.Config.Image;
                if (isUpdated)
                {
                    savedProduct.Config.ProductId = product.Id;
                    unitOfWork.ProductRepository.Update(savedProduct);
                }
                else
                {
                    var p = unitOfWork.ProductRepository.Add(savedProduct);
                    product.Id = p.Id;
                }
                var result = unitOfWork.Completed();
                if (result > 0)
                {
                    _eventAggregator.GetEvent<ProductSaveCompletedEvent>().Publish(product);
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
            if (product == null)
            {
                Product = new Product {Config = new Models.Config()};

            }
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
                    Brand = Brands.Take(1).FirstOrDefault()
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
                        //Categories.Add(Mapper.Map<Category>(category));
                        Categories.Add(new Category
                        {
                            Id = category.Id,
                            Name = category.Name
                        });
                    }
                }
                if (Brands == null)
                {
                    Brands = new List<Brand>();
                    foreach (var brand in unitOfWork.BrandRepository.GetAll())
                    {
                        //Brands.Add(Mapper.Map<Brand>(brand));
                        Brands.Add(new Brand
                        {
                            Id   = brand.Id,
                            Name = brand.Name
                        });
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