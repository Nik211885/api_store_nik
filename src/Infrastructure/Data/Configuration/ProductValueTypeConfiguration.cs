using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductValueTypeConfiguration : IEntityTypeConfiguration<ProductValueType>
    {
        public void Configure(EntityTypeBuilder<ProductValueType> builder)
        {
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
