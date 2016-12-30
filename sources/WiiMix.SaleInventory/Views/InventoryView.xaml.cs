using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Controls;

namespace WiiMix.SaleInventory.Views
{
    /// <summary>
    /// Interaction logic for InventoryView
    /// </summary>
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
        }

        private void HamburgerMenu_OnItemClick(object sender, ItemClickEventArgs e)
        {
            HamburgerMenuControl.Content = e.ClickedItem;
        }

        private async void HamburgerMenu_OnOptionsItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as HamburgerMenuItem;
            if (menuItem != null)
                await this.TryFindParent<MetroWindow>().ShowMessageAsync("", $"You clicked on {menuItem.Label} button");
        }
    }
}
