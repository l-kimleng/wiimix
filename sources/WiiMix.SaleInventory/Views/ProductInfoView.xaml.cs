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

        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    var field = typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic);
        //    if (field != null)
        //        field.SetValue(this, false);
        //    e.Cancel = true;
        //    Hide();
        //}
    }
}
