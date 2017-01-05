using System.Data.Entity;
using WiiMix.Data.Entities;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly SaleInventoryContext _context;
        public BrandRepository(DbContext context) : base(context)
        {
            _context = context as SaleInventoryContext;
        }
    }
}