using Microsoft.Practices.Prism.Mvvm;

namespace WiiMix.SaleInventory
{
    public interface IDialogService : IView
    {
        void ShowPopup();
        void ClosePopup();
    }
}