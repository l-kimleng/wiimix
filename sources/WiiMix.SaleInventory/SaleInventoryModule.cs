using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using WiiMix.SaleInventory.Utils;

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
            var region = _regionManager.Regions[RegionNameCollection.CoreRegion];
        }
    }
}
