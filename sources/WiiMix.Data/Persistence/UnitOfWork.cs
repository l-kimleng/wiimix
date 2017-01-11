using WiiMix.Data.Persistence.Repositories;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        // We are not using dbContext because we are going to 
        // work with more than two dbContext in unit of work
        private readonly SaleInventoryContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IBrandRepository _brandRepository;
        private IConfigRepository _configRepository;
        private IStockRepository _stockRepository;

        public UnitOfWork(SaleInventoryContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_context));
        public ICategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context));
        public IBrandRepository BrandRepository => _brandRepository ?? (_brandRepository = new BrandRepository(_context));
        public IConfigRepository ConfigRepository => _configRepository ?? (_configRepository = new ConfigRepository(_context));
        public IStockRepository StockRepository => _stockRepository ?? (_stockRepository = new StockRepository(_context));

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
