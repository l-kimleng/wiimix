using Microsoft.Practices.Prism.Mvvm;

namespace WiiMix.SaleInventory.Interface
{
    public interface IDialogService : IView
    {
        void ShowPopup();
        void ClosePopup();
    }
}