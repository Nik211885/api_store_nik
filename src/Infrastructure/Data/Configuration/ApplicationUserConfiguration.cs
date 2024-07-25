using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Id)
                .HasMaxLength(40)
                .IsRequired();

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.HasMany(u => u.UserClaims)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .IsRequired();

            builder.HasMany(u => u.UserLogins)
            .WithOne(l => l.User)
            .HasForeignKey(l => l.UserId)
            .IsRequired();

            builder.HasMany(u => u.UserTokens)
            .WithOne(ut => ut.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

            builder.HasMany(u => u.Carts)
            .WithOne()
            .HasForeignKey(c => c.UserId)
            .IsRequired();

            builder.HasMany(u => u.Products)
            .WithOne()
            .HasForeignKey(p => p.UserId)
            .IsRequired();

            builder.HasMany(u => u.PromotionDiscounts)
            .WithOne()
            .HasForeignKey(prd => prd.UserId)
            .IsRequired();

            builder.HasMany(u => u.Ratings)
            .WithOne()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

            builder.HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.MessageFrom)
            .IsRequired();

            builder.HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.MessageTo)
            .IsRequired();
            builder.HasMany(u => u.AddressStates)
            .WithOne()
            .HasForeignKey(a=>a.UserId)
            .IsRequired();
        }
    }
}
