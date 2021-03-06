﻿using System;
using System.Collections.Generic;

namespace WiiMix.Data.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual ICollection<StockDetail> Details { get; set; }
    }
}