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

        public Product()
        {
            Config = new Config();
        }
    }
}