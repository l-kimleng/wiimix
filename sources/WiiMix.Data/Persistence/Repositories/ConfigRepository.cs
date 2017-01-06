using System.Data.Entity;
using WiiMix.Data.Entities;
using WiiMix.Data.Repositories;

namespace WiiMix.Data.Persistence.Repositories
{
    public class ConfigRepository : Repository<Config>, IConfigRepository
    {
        private readonly SaleInventoryContext _context;
        public ConfigRepository(DbContext context) : base(context)
        {
            _context = context as SaleInventoryContext;
        }
    }
}