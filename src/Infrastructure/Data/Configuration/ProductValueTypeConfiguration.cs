using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductValueTypeConfiguration : IEntityTypeConfiguration<ProductValueType>
    {
        public void Configure(EntityTypeBuilder<ProductValueType> builder)
        {
            builder.HasMany(x => x.OrderDetailProductValueTypes)
                .WithOne(x => x.ProductValueType)
                .HasForeignKey(x => x.ProductValueTypeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
