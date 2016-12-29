using System.Data.Entity.ModelConfiguration;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Persistence.EntityConfigurations
{
    public class ConfigConfiguration : EntityTypeConfiguration<Config>
    {
        public ConfigConfiguration()
        {
            Property(x => x.Feature)
                .HasColumnType("NVarChar")
                .HasMaxLength(300);

            Property(x => x.Price)
                .HasColumnType("Money");

            Property(x => x.Image)
                .HasColumnType("VarChar")
                .HasMaxLength(125);

            HasKey(x => x.ProductId);
        }
    }
}