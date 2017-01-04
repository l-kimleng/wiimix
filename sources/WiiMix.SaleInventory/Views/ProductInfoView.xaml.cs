using WiiMix.SaleInventory.ViewModels;

namespace WiiMix.SaleInventory.Views
{
    /// <summary>
    /// Interaction logic for ProductInfoView.xaml
    /// </summary>
    public partial class ProductInfoView : IProductInfoView
    {
        public ProductInfoView(ProductInfoViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public void Display()
        {
            ShowDialog();
        }
    }
}
