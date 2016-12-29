namespace WiiMix.Data.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        // We are not using dbContext because we are going to 
        // work with more than two dbContext in unit of work
        private readonly SaleInventoryContext _context;

        public UnitOfWork(SaleInventoryContext context)
        {
            _context = context;
        }

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
