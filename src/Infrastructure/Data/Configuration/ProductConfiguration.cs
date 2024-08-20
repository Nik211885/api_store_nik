using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasMany(p => p.ProductPromotionDiscounts)
                .WithOne(p=>p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.ProductNameTypes)
                .WithOne(ppd => ppd.Product)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.OrderDetails)
                .WithOne(ppd => ppd.Product)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.ProductId);

            builder.HasMany(p => p.Ratings)
                .WithOne(ppd => ppd.Product)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(p => p.ProductId);
            builder.HasMany(p => p.ProductDescriptions)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId)
                .IsRequired();
        }
    }
}
