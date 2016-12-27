using Prism.Mvvm;
using WiiMix.SaleInventory.Utils;

namespace WiiMix.SaleInventory.ViewModels
{
    public class InventoryViewModel : BindableBase
    {
        public InventoryViewModel()
        {

        }

        public string Title
        {
            get { return ModuleConstantCollection.InventoryHeaderTitle; }
        }
    }
}
