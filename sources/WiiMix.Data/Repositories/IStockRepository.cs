using System.Collections.Generic;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {
        IEnumerable<Stock> FindAllDetail();
        Stock FindUpdate(int stockId);
    }
}