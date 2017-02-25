namespace WiiMix.Data.Entities
{
    public class ItemTransfer
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public float Quantity { get; set; }
        public decimal Cost { get; set; }

        public virtual Product Product { get; set; }
        public virtual Stock Stock { get; set; }
    }
}