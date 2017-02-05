using System.Collections.Generic;
using WiiMix.Business.Model;

namespace WiiMix.SaleInventory.Service
{
    public interface IStockService
    {
        IEnumerable<Stock> FindAllDetail();
        Stock Add(Stock stock);
        int Update(Stock stock);
    }
}