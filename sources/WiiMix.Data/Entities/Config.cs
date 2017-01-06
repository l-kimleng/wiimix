﻿namespace WiiMix.Data.Entities
{
    public class Config
    {
        public string Feature { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public Config Clone()
        {
            return new Config
            {
                ProductId = ProductId,
                Feature = Feature,
                Price = Price,
                Image = Image
            };
        }
    }
}