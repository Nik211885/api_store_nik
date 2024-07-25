using ApplicationCore.Entities.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasMany(r => r.Reactions)
                .WithOne(r => r.Rating)
                .HasForeignKey(r => r.RatingId)
                .IsRequired();
        }
    }
}
