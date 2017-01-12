using Prism.Mvvm;
using WiiMix.SaleInventory.Utils;

namespace WiiMix.SaleInventory.ViewModels
{
    public class SaleViewModel : BindableBase
    {
        public SaleViewModel()
        {
        }

        public string Title => ModuleConstantCollection.SaleHeaderTitle;
    }
}
