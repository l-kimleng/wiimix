namespace WiiMix.Data.Entities
{
    public class StockConfig
    {
        public int Id { get; set; }

        public int ConfigId { get; set; }
        public virtual Config Config { get; set; }
        public double Quantity { get; set; }

        public int StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}