using AutoMapper;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using WiiMix.Data;
using WiiMix.Data.Entities;
using WiiMix.Data.Persistence;
using WiiMix.Data.Persistence.Repositories;
using WiiMix.Data.Repositories;
using WiiMix.SaleInventory.Utils;
using WiiMix.SaleInventory.ViewModels;
using WiiMix.SaleInventory.Views;

namespace WiiMix.SaleInventory
{
    public class SaleInventoryModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public SaleInventoryModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IUnitOfWork, UnitOfWork>(new TransientLifetimeManager());
            _container.RegisterType<IProductRepository, ProductRepository>();
            _container.RegisterType<ICategoryRepository, CategoryRepository>();
            _container.RegisterType<IBrandRepository, BrandRepository>();
            _container.RegisterType<IStockRepository, StockRepository>();

            _container.RegisterTypeForNavigation<MainView>();
            _container.RegisterTypeForNavigation<ProductView>("Inventory/ProductView");
            _container.RegisterTypeForNavigation<StockView>("Inventory/StockView");
            _container.RegisterType<IProductInfoView, ProductInfoView>(new TransientLifetimeManager());
            _container.RegisterType<IProductInfoViewModel, ProductInfoViewModel>();
            _container.RegisterType<IStockInfoView, StockInfoView>(new TransientLifetimeManager());
            _container.RegisterType<IStockInfoViewModel, StockInfoViewModel>();

            InitializeMapper();

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => _container.Resolve(type));

            var region = _regionManager.Regions[RegionConstantCollection.CoreRegion];
            region.Add(_container.Resolve<MainView>());

            _regionManager.RegisterViewWithRegion(RegionConstantCollection.MainRegion, typeof(InventoryView));
            _regionManager.RegisterViewWithRegion(RegionConstantCollection.MainRegion, typeof(SaleView));

            _regionManager.RegisterViewWithRegion(RegionConstantCollection.InventoryRegion, typeof(ProductView));
            _regionManager.RegisterViewWithRegion(RegionConstantCollection.InventoryRegion, typeof(StockView));
            
            _regionManager.RequestNavigate(RegionConstantCollection.CoreRegion, typeof(MainView).FullName);
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(x => x.CreateMap<Category, Models.Category>().ForMember(c => c.Products, opt => opt.Ignore()));
            Mapper.Initialize(x => x.CreateMap<Brand, Models.Brand>().ForMember(b => b.Products, opt => opt.Ignore()));
            Mapper.Initialize(x => x.CreateMap<Config, Models.Config>().ForMember(c => c.Product, opt => opt.Ignore()));
            Mapper.Initialize(x => x.CreateMap<Product, Models.Product>().ForMember(p => p.Details, opt => opt.Ignore()));
        }
    }
}
