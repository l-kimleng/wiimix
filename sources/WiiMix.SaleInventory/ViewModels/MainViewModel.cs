using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using WiiMix.SaleInventory.Utils;
using WiiMix.SaleInventory.Views;

namespace WiiMix.SaleInventory.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public MainViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }
        

        private void LoadViews()
        {
            var region = _regionManager.Regions[RegionNameCollection.MainRegion];

            region.Add(_container.Resolve<InventoryView>());
            region.Add(_container.Resolve<SaleView>());

            _regionManager.RequestNavigate(RegionNameCollection.MainRegion, typeof(InventoryView).FullName);

        }
    }
}
