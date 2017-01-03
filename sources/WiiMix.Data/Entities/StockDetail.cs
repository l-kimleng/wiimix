namespace WiiMix.Data.Entities
{
    public class StockDetail
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }

        public float Quantity { get; set; }
    }
}