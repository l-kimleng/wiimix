using System.Data.Entity.ModelConfiguration;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Persistence.EntityConfigurations
{
    public class BrandConfiguration : EntityTypeConfiguration<Brand>
    {
        public BrandConfiguration()
        {
            Property(x => x.Name)
                .HasColumnType("VarChar")
                .HasMaxLength(125)
                .IsRequired();
        }
    }
}