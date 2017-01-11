namespace WiiMix.SaleInventory.Views
{
    /// <summary>
    /// Interaction logic for StockInfoView.xaml
    /// </summary>
    public partial class StockInfoView : IStockInfoView
    {
        public StockInfoView()
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
