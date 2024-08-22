using ApplicationCore.Entities.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder.HasOne(x => x.Rating)
                .WithMany(x => x.Reactions)
                .HasForeignKey(x => x.RatingId)
                .IsRequired();
            builder.HasKey(x => new { x.UserId, x.RatingId });
        }
    }
}
