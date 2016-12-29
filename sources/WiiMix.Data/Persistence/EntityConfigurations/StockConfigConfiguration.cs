using System.Data.Entity.ModelConfiguration;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Persistence.EntityConfigurations
{
    public class StockConfigConfiguration : EntityTypeConfiguration<StockConfig>
    {
        public StockConfigConfiguration()
        {
            Property(x => x.Quantity)
                .HasColumnType("Real")
                .IsRequired();
        }
    }
}