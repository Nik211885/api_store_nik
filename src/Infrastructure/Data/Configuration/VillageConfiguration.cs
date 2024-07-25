using ApplicationCore.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class VillageConfiguration : IEntityTypeConfiguration<Village>
    {
        public void Configure(EntityTypeBuilder<Village> builder)
        {
            builder.HasMany(v => v.Addresses)
                .WithOne(a => a.Village)
                .HasForeignKey(a => a.VillageId)
                .IsRequired();
        }
    }
}
