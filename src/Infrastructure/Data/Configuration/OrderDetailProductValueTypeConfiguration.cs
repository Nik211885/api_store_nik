using ApplicationCore.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class OrderDetailProductValueTypeConfiguration
        : IEntityTypeConfiguration<OrderDetailProductValueType>
    {
        public void Configure(EntityTypeBuilder<OrderDetailProductValueType> builder)
        {
            builder.HasOne(x => x.OrderDetail)
                .WithMany(o => o.OrderDetailProductValueTypes)
                .HasForeignKey(x => x.OrderDetailId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasOne(x => x.ProductValueType)
                .WithMany(o => o.OrderDetailProductValueTypes)
                .HasForeignKey(x => x.OrderDetailId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.HasKey(x => new { x.ProductValueTypeId,x.OrderDetailId });
        }
    }
}
