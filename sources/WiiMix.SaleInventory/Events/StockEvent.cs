using Prism.Events;
using WiiMix.SaleInventory.Models;

namespace WiiMix.SaleInventory.Events
{
    public class StockLoadedEvent : PubSubEvent<Stock>
    {
    }
}
