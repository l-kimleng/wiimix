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
            UpdateCommand = new DelegateCommand<Product>(OnUpdateProduct);
            AddNewCommand = new DelegateCommand(OnAddNewProduct);
        }

        private void OnAddNewProduct()
        {
        }

        private void OnUpdateProduct(Product product)
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var savedProduct = unitOfWork.ProductRepository.FindUpdate(product.Id);
                savedProduct.Name = product.Name;
                savedProduct.CategoryId = product.Category.Id;
                savedProduct.BrandId = product.Brand.Id;

                savedProduct.Config.Feature = product.Config.Feature;
                savedProduct.Config.Price = product.Config.Price;
                savedProduct.Config.Image = product.Config.Image;
                savedProduct.Config.ProductId = product.Id;

                unitOfWork.ProductRepository.Update(savedProduct);
                var result = unitOfWork.Completed();
                if (result > 0)
                {
                    //var payload = Mapper.Map<Product>(savedProduct);
                    //var payload = new Product
                    //{
                    //    Id = savedProduct.Id,
                    //    Name = savedProduct.Name,
                    //    CategoryId = savedProduct.CategoryId,
                    //    BrandId = savedProduct.BrandId,
                    //    Category = new Category
                    //    {
                    //        Id = savedProduct.CategoryId,
                    //        Name = savedProduct.Category.Name
                    //    },
                    //    Brand = new Brand
                    //    {
                    //        Id = savedProduct.BrandId,
                    //        Name = savedProduct.Brand.Name
                    //    },
                    //    Config = new Config
                    //    {
                    //        ProductId = savedProduct.Id,
                    //        Feature = savedProduct.Config.Feature,
                    //        Price = savedProduct.Config.Price,
                    //        Image = savedProduct.Config.Image
                    //    }
                    //};
                    _eventAggregator.GetEvent<ProductUpdateCompletedEvent>().Publish(product);
                }
            }
        }

        private void OnCancelProduct()
        {
            CloseDialog();
        }

        private void OnLoadedProdcut(Product updateProduct)
        {
            Title = updateProduct == null ? "Create Product" : "Update Product";
            Product = updateProduct;
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
        public ICommand UpdateCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }

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