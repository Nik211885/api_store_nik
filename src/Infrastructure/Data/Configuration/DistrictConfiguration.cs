using ApplicationCore.Entities.Address;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasMany(d => d.Villages)
                .WithOne(v => v.District)
                .HasForeignKey(v => v.DistrictId)
                .IsRequired();
        }
    }
}
