namespace WiiMix.Entity
{
    public class Config
    {
        public int Id { get; set; }
        public string Feature { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}