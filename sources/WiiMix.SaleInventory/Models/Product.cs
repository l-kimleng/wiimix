using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;

namespace WiiMix.SaleInventory.Models
{
    public class Product : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _categoryId;
        public int CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        public virtual Category Category { get; set; }

        private int _bandId;
        public int BrandId
        {
            get { return _bandId; }
            set { SetProperty(ref _bandId, value); }
        }

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