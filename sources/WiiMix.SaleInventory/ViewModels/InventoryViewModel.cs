using MahApps.Metro.Controls;
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
        public InventoryViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            ItemClickCommand = new DelegateCommand<object>(OnItemClick);
        }

        private void OnItemClick(object obj)
        {
            var hamburgerMenu = obj as HamburgerMenu;
            if (hamburgerMenu == null) return;
            var selectedItem = hamburgerMenu.SelectedItem;
            var menu = selectedItem as HamburgerMenuItem;
            _regionManager.RequestNavigate(RegionConstantCollection.InventoryRegion, menu?.Tag.ToString() ?? "");
        }

        public string Title => ModuleConstantCollection.InventoryHeaderTitle;

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
