using System.Collections.Generic;

namespace WiiMix.Data.Entities
{
    public class StockDetail
    {
        public int ProductId { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public int StockId { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

        public float Quantity { get; set; }
    }
}