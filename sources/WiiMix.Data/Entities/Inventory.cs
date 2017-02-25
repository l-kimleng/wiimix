namespace WiiMix.Data.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public decimal Cost { get; set; }

        public virtual Product Product { get; set; }
    }
}