using System.Data.Entity;
using WiiMix.Data.Entities;
using WiiMix.Data.Persistence.EntityConfigurations;

namespace WiiMix.Data.Persistence
{
    public class SaleInventoryContext : DbContext
    {
        public SaleInventoryContext() : base("name=SaleInventoryContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        //public virtual DbSet<StockDetail> StockConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new BrandConfiguration());
            modelBuilder.Configurations.Add(new ConfigConfiguration());
            modelBuilder.Configurations.Add(new StockConfiguration());
            //modelBuilder.Configurations.Add(new StockDetailConfiguration());
        }
    }
}
