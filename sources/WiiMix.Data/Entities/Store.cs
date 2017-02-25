namespace WiiMix.Data.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public decimal Price { get; set; }
        public int StockTransferId { get; set; }

        public virtual Product Product { get; set; }
        public virtual StockTransfer StockTransfer { get; set; }
    }
}