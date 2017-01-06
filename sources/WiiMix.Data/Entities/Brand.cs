using System.Collections.Generic;

namespace WiiMix.Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Brand Clone()
        {
            return new Brand
            {
                Id = Id,
                Name = Name
            };
        }
    }
}