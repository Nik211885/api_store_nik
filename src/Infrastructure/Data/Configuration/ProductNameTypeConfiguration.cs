using ApplicationCore.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProductNameTypeConfiguration : IEntityTypeConfiguration<ProductNameType>
    {
        public void Configure(EntityTypeBuilder<ProductNameType> builder)
        {
            builder.HasMany(pn => pn.ProductValueTypes)
                .WithOne(pv => pv.ProductNameType)
                .HasForeignKey(pv => pv.ProductNameTypeId)
                .IsRequired();
        }
    }
}
