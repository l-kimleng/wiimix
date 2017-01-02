using System.Data.Entity.ModelConfiguration;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Persistence.EntityConfigurations
{
    public class StockDetailConfiguration : EntityTypeConfiguration<StockDetail>
    {
        public StockDetailConfiguration()
        {
            Property(x => x.Quantity)
                .HasColumnType("Real")
                .IsRequired();

            HasKey(x => new {x.ProductId, x.StockId});
        }
    }
}