﻿using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Unity;
using WiiMix.SaleInventory.Utils;
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
            _container.RegisterTypeForNavigation<MainView>();
            _container.RegisterType<object,ProductView>("Inventory/ProductView");
            _container.RegisterType<object,StockView>("Inventory/StockView");

            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => _container.Resolve(type));

            var region = _regionManager.Regions[RegionConstantCollection.CoreRegion];
            region.Add(_container.Resolve<MainView>());

            _regionManager.RegisterViewWithRegion(RegionConstantCollection.MainRegion, typeof(InventoryView));
            _regionManager.RegisterViewWithRegion(RegionConstantCollection.MainRegion, typeof(SaleView));

            _regionManager.RegisterViewWithRegion(RegionConstantCollection.InventoryRegion, typeof(ProductView));
            _regionManager.RegisterViewWithRegion(RegionConstantCollection.InventoryRegion, typeof(StockView));
            
            _regionManager.RequestNavigate(RegionConstantCollection.CoreRegion, typeof(MainView).FullName);
        }
    }
}
