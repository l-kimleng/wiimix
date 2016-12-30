using Prism.Mvvm;
using Prism.Regions;

namespace WiiMix.SaleInventory.ViewModels
{
    public class ProductViewModel : BindableBase, INavigationAware
    {
        public ProductViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var context = navigationContext;
            string par = "";
            foreach (var p in context.Parameters)
            {
                par = (string)p.Value;
                break;
            }
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            var context = navigationContext;
            string par = "";
            foreach (var p in context.Parameters)
            {
                par = (string)p.Value;
                break;
            }
        }
    }
}
