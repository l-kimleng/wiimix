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

        private Category _category;
        public Category Category
        {
            get{return _category;}
            set { SetProperty(ref _category, value); }
        }

        private int _bandId;
        public int BrandId
        {
            get { return _bandId; }
            set { SetProperty(ref _bandId, value); }
        }

        private Brand _brand;
        public Brand Brand
        {
            get { return _brand; }
            set { SetProperty(ref _brand, value); }
        }

        private Config _config;
        public Config Config
        {
            get { return _config; }
            set { SetProperty(ref _config, value); }
        }

        public ICollection<StockDetail> Details { get; set; }

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