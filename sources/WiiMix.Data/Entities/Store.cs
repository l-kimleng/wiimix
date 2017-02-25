using System.Collections.Generic;

namespace WiiMix.Data.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public ICollection<StoreItem> StoreItems { get; set; }
    }
}