using WiiMix.SaleInventory.Interface;

namespace WiiMix.SaleInventory.Views
{
    /// <summary>
    /// Interaction logic for ProductInfoView.xaml
    /// </summary>
    public partial class ProductInfoView : IProductInfoView
    {
        public ProductInfoView()
        {
            InitializeComponent();
        }

        public void ShowPopup()
        {
            ShowDialog();
        }

        public void ClosePopup()
        {
            Close();
        }
    }
}
