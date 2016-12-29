namespace WiiMix.Data.Entities
{
    public class StockConfig
    {
        public int ConfigId { get; set; }
        public virtual Config Config { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }

        public float Quantity { get; set; }
    }
}