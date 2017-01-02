using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WiiMix.Data.Entities;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly SaleInventoryContext _context;
        public ProductRepository(DbContext context) : base(context)
        {
            _context = context as SaleInventoryContext;
        }

        public IEnumerable<Product> Display()
        {
            var products = _context.Products;
            return products.Include(c => c.Category)
                .Include(b => b.Brand)
                .Include(c => c.Config).ToList();
        }
    }
}
