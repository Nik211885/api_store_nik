using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class PromotionDiscountConfiguration : IEntityTypeConfiguration<PromotionDiscount>
    {
        public void Configure(EntityTypeBuilder<PromotionDiscount> builder)
        {
            builder.HasMany(p => p.ProductPromotionDiscounts)
                .WithOne(p=>p.PromotionDiscount)
                .HasForeignKey(p => p.PromotionDiscountId)
                .IsRequired();
            builder.Property(p => p.Promotion)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
