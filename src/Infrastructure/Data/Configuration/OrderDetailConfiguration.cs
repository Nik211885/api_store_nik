using ApplicationCore.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.PriceProduct)
                .HasColumnType("decimal(18,2)");
            builder.Property(x => x.PricePromotion)
                .HasColumnType("decimal(18,2)");
        }
    }
}
