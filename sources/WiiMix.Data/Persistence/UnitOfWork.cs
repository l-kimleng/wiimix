using WiiMix.Data.Persistence.Repositories;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        // We are not using dbContext because we are going to 
        // work with more than two dbContext in unit of work
        private readonly SaleInventoryContext _context;
        private IProductRepository _products;
        private ICategoryRepository _categories;
        private IBrandRepository _brands;
        private IConfigRepository _configs;
        private IStockRepository _stocks;

        public UnitOfWork(SaleInventoryContext context)
        {
            _context = context;
        }

        public IProductRepository Products => _products ?? (_products = new ProductRepository(_context));
        public ICategoryRepository Categories => _categories ?? (_categories = new CategoryRepository(_context));
        public IBrandRepository Brands => _brands ?? (_brands = new BrandRepository(_context));
        public IConfigRepository Configs => _configs ?? (_configs = new ConfigRepository(_context));
        public IStockRepository Stocks => _stocks ?? (_stocks = new StockRepository(_context));

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Completed()
        {
            return _context.SaveChanges();
        }
    }
}
