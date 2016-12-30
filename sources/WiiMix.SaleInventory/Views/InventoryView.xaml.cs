using MahApps.Metro.Controls;
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
            var myMenuItem = e.ClickedItem as HamburgerMenuItem;
            var myView = myMenuItem.Tag.ToString();
        }
    }
}
