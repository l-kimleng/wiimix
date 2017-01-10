using Prism.Events;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.Events
{
    public class ProductLoadedEvent : PubSubEvent<Product>
    {
    }

    public class ProductSaveCompletedEvent : PubSubEvent<Product>
    {
        
    }
}
