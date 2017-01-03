using Prism.Events;
using WiiMix.Data.Entities;

namespace WiiMix.SaleInventory.Events
{
    public class ProductUpdatedEvent : PubSubEvent<Product>
    {
    }
}
