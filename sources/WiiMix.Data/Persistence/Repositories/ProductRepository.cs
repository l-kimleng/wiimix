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

        public IEnumerable<Product> Find()
        {
            var products = _context.Products.Include(c => c.Category).Include(b => b.Brand);
            var configs = _context.Configs.ToList();
            IList<Product> productList = new List<Product>();
            foreach (var product in products)
            {
                var config = configs.FirstOrDefault(x => x.ProductId == product.Id);
                product.Update(config);
                productList.Add(product);
            }
            return productList;
        }

        public Product FindUpdate(int productId)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == productId);
            if (product != null)
            {
                var config = _context.Configs.SingleOrDefault(c => c.ProductId == productId);
                product.Update(config);
            }
            return product;
        }
    }
}
