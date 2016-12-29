using System;
using System.Collections.Generic;

namespace WiiMix.Entity
{
    public class Stock
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual ICollection<StockConfig> Details { get; set; }
    }
}