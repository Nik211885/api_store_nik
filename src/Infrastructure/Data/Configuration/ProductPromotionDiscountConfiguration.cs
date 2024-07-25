using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductPromotionDiscountConfiguration : IEntityTypeConfiguration<ProductPromotionDiscount>
    {
        public void Configure(EntityTypeBuilder<ProductPromotionDiscount> builder)
        {
            builder.HasKey(ppd => new { ppd.ProductId, ppd.PromotionDiscountId });
            builder.HasOne(ppd => ppd.Product)
                .WithMany(p => p.ProductPromotionDiscounts)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
