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
        private readonly IUnitOfWork _unitOfWork;
        public ProductInfoViewModel(IUnityContainer container, IEventAggregator eventAggregator, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;
            eventAggregator.GetEvent<ProductUpdatedEvent>().Subscribe(OnUpdatedProdcut);

            Initialize();
            CancelCommand = new DelegateCommand(OnCancelProduct);
            SaveCommand = new DelegateCommand<Product>(OnSaveProduct);
        }

        private void OnSaveProduct(Product savedProduct)
        {
            using (var unitOfWork = _unitOfWork)
            {
                var productRepository = unitOfWork.ProductRepository;
                productRepository.Add(savedProduct);
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
            using (var unitOfWork = _unitOfWork)
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
