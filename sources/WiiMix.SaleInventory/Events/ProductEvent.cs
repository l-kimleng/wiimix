using Prism.Events;
using WiiMix.Business.Model;

namespace WiiMix.SaleInventory.Events
{
    public class ProductLoadedEvent : PubSubEvent<Product>
    {
    }

    public class ProductUpdateCompletedEvent : PubSubEvent<Product>
    {
        
    }

    public class ProductCreateCompletedEvent : PubSubEvent<Product>
    {
        
    }
}
