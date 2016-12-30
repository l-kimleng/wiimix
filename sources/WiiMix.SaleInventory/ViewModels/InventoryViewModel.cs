using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Input;
using WiiMix.SaleInventory.Utils;

namespace WiiMix.SaleInventory.ViewModels
{
    public class InventoryViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private static bool test = false;

        public InventoryViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ItemClickCommand = new DelegateCommand<object>(OnItemClick);
        }

        private void OnItemClick(object obj)
        {
            var str = (test) ? "Inventory/StockView" : "Inventory/ProductView";
            _regionManager.RequestNavigate(Utils.RegionConstantCollection.InventoryRegion, str);
            test = !test;
        }

        public string Title
        {
            get { return ModuleConstantCollection.InventoryHeaderTitle; }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public ICommand ItemClickCommand { get; private set; }
    }
}
