using Prism.Events;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.Events
{
    public class ProductLoadedEvent : PubSubEvent<Product>
    {
    }

    public class ProductUpdateCompletedEvent : PubSubEvent<Product>
    {
        
    }

    public class ProductAddCompletedEvent : PubSubEvent<Product>
    {
        
    }
}
