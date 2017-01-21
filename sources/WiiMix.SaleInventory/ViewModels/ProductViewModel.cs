using AutoMapper;
using OfficeOpenXml;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using WiiMix.Data;
using WiiMix.SaleInventory.Events;
using WiiMix.SaleInventory.Interface;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnitOfWork _unitOfWork;
        public ProductViewModel(IEventAggregator eventAggregator, IUnitOfWork unitOfWork)
        {
            _eventAggregator = eventAggregator;
            _unitOfWork = unitOfWork;
            GetAll();
            UpdateCommand = new DelegateCommand<Product>(OnClickUpdatedCommand);
            AddNewCommand = new DelegateCommand(OnAddNewProductCommand);
            ExportCommand = new DelegateCommand<IEnumerable<Product>>(OnExportProductsCommand);
            eventAggregator.GetEvent<ProductUpdateCompletedEvent>().Subscribe(OnSaveProductCompleted);
            eventAggregator.GetEvent<ProductCreateCompletedEvent>().Subscribe(OnSaveProductCompleted);
        }

        private void OnExportProductsCommand(IEnumerable<Product> products)
        {
            var productList = products.ToList();
            if (productList.Any())
            {
                var templatePath = Properties.Resources.ProductTemplate;
                var outputFolder = Properties.Resources.OutputFolder;
                var outputFile = outputFolder + @"\_product_list.xlsx";
                var newFile = new FileInfo(outputFile);
                var template = new FileInfo(templatePath);
                using (var xlPackage = new ExcelPackage(newFile, template))
                {
                    var worksheet = xlPackage.Workbook.Worksheets["Product List"];
                    var row = 2;
                    foreach (var product in productList)
                    {
                        worksheet.Cells[row, 1].Value = product.Id;
                        worksheet.Cells[row, 2].Value = product.Name;
                        worksheet.Cells[row, 3].Value = product.Config.Image;
                        worksheet.Cells[row, 4].Value = product.Category.Name;
                        worksheet.Cells[row, 5].Value = product.Brand.Name;
                        worksheet.Cells[row, 6].Value = product.Config.Feature;
                        worksheet.Cells[row, 7].Value = product.Config.Price;
                        row++;
                    }
                    xlPackage.Save();
                }
            }
        }

        private bool _isUpdated = true;
        private void OnAddNewProductCommand()
        {
            _isUpdated = false;
            _eventAggregator.GetEvent<ProductLoadedEvent>().Publish(null);
        }

        private void OnClickUpdatedCommand(Product selectedProduct)
        {
            var product = selectedProduct.Clone();
            _isUpdated = true;
            _eventAggregator.GetEvent<ProductLoadedEvent>().Publish(product);
        }

        private void OnSaveProductCompleted(Product product)
        {
            if (_isUpdated)
            {
                SelectedProduct.Name = product.Name;
                SelectedProduct.BrandId = product.BrandId;
                SelectedProduct.CategoryId = product.CategoryId;
                SelectedProduct.Category.Id = product.CategoryId;
                SelectedProduct.Category.Name = product.Category.Name;
                SelectedProduct.Brand.Id = product.BrandId;
                SelectedProduct.Brand.Name = product.Brand.Name;
                SelectedProduct.Config.ProductId = product.Config.ProductId;
                SelectedProduct.Config.Feature = product.Config.Feature;
                SelectedProduct.Config.Image = product.Config.Image;
                SelectedProduct.Config.Price = product.Config.Price;
            }
            else
            {
                Products.Add(product);
            }
        }

        [Microsoft.Practices.Unity.Dependency]
        public IProductInfoViewModel ProductInfoViewModel { get; set; }

        private void GetAll()
        {
            using (var unitOfwork = _unitOfWork)
            {
                var productRepository = unitOfwork.ProductRepository;
                Products = new ObservableCollection<Product>();
                //Products.AddRange(Mapper.Map<IEnumerable<Product>>(productRepository.Find()));
                foreach (var product in productRepository.Find())
                {
                    Products.Add(Mapper.Map<Product>(product));
                }
                ProductCollectionView = CollectionViewSource.GetDefaultView(Products);
                if (Products.Count > 0)
                {
                    SelectedProduct = Products[0];
                }
            }
        }

        private ICollectionView _productCollectionView;
        public ICollectionView ProductCollectionView
        {
            get { return _productCollectionView; }
            set { SetProperty(ref _productCollectionView, value); }
        }

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        public ICommand UpdateCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
    }
}
