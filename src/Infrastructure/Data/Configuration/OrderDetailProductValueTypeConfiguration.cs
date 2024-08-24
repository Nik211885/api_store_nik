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
                .IsRequired();
            builder.HasKey(x => new { x.ProductValueTypeId,x.OrderDetailId });
        }
    }
}
