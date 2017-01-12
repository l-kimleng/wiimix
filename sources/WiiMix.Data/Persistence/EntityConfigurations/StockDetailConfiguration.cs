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

            Property(x => x.Price)
                .HasColumnType("Money")
                .IsRequired();

            HasRequired(x => x.Product)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.Stock)
                .WithMany(x => x.Details)
                .HasForeignKey(x => x.StockId)
                .WillCascadeOnDelete(false);
        }
    }
}