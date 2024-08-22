using ApplicationCore.Entities.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasOne(x=>x.OrderDetail)
                .WithOne(x=>x.Rating)
                .HasForeignKey<Rating>(x=>x.OrderDetailId)
                .IsRequired();
        }
    }
}
