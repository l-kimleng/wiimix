using Prism.Events;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.Events
{
    public class StockLoadedEvent : PubSubEvent<Stock>
    {
    }

    public class StockCreateCompletedEvent : PubSubEvent<Stock>
    {
        
    }

    public class StockUpdateCompletedEvent : PubSubEvent<Stock>
    {
        
    }
}
