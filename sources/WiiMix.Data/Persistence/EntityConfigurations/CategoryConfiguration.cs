using System.Data.Entity.ModelConfiguration;
using WiiMix.Data.Entities;

namespace WiiMix.Data.Persistence.EntityConfigurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(x => x.Name)
                .HasColumnType("VarChar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}