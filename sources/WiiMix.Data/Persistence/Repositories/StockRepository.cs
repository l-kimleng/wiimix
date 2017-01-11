using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WiiMix.Data.Entities;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        private readonly SaleInventoryContext _context;
        public StockRepository(DbContext context) : base(context)
        {
            _context = context as SaleInventoryContext;
        }


        public IEnumerable<Stock> FindAllDetail()
        {
            var stocks = _context.Stocks.Include(s => s.Details);
            return stocks.ToList();
        }
    }
}