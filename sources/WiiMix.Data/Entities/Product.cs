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

            Config = config.Clone();
        }

        public Product Clone()
        {
            var product = new Product()
            {
                Id = Id,
                Name = Name,
                CategoryId = CategoryId,
                BrandId = BrandId,
                Category = Category.Clone(),
                Brand = Brand.Clone(),
                Config = Config.Clone()
            };
            return product;
        }
    }
}