using System.Data.Entity;
using WiiMix.Data.Entities;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly SaleInventoryContext _context;
        public CategoryRepository(DbContext context) : base(context)
        {
            _context = context as SaleInventoryContext;
        }
    }
}
