using System.Data.Entity.ModelConfiguration;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Persistence.EntityConfigurations
{
    public class StockConfiguration : EntityTypeConfiguration<Stock>
    {
        public StockConfiguration()
        {
            Property(x => x.Date)
                .HasColumnType("DateTime")
                .IsRequired();

            Property(x => x.Quantity)
                .HasColumnType("Real");

            Property(x => x.TotalPrice)
                .HasColumnType("Money")
                .HasPrecision(byte.Parse("2"), byte.Parse("5"));

            HasMany(x => x.Details)
                .WithMany(x => x.Stocks)
                .Map(m =>
                {
                    m.ToTable("StockConfig");
                    m.MapLeftKey("StockId");
                    m.MapRightKey("ConfigId");
                });
        }
    }
}