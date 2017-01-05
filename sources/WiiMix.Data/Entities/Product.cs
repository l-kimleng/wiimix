using System.Collections.Generic;

namespace WiiMix.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual Config Config { get; set; }

        public virtual ICollection<StockDetail> Details { get; set; }

        public void Update(Config config)
        {
            if (config == null)
            {
                Config = new Config();
                return;
            }

            Config = new Config
            {
                Feature = config.Feature,
                Price = config.Price,
                Image = Config.Image,
                ProductId = config.ProductId
            };
        }

        public Product Clone()
        {
            var other = (Product)MemberwiseClone();
            other.Id = Id;
            other.Name = Name;
            other.CategoryId = CategoryId;
            other.BrandId = BrandId;
            other.Category = new Category {Id = Category.Id, Name = Category.Name};
            other.Brand = new Brand {Id = Brand.Id, Name = Brand.Name};
            other.Config = new Config
            {
                ProductId = Config.ProductId,
                Feature = Config.Feature,
                Image = Config.Feature,
                Price = Config.Price
            };
            return other;
        }
    }
}