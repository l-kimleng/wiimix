using Microsoft.Practices.Prism.Mvvm;

namespace WiiMix.SaleInventory.Models
{
    public class Config : BindableBase
    {
        private string _feature;
        public string Feature
        {
            get { return _feature; }
            set { SetProperty(ref _feature, value); }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

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