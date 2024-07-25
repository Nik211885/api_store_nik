using ApplicationCore.Entities;
using ApplicationCore.Entities.Address;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreNikDbConText : IdentityDbContext<ApplicationUser,
        ApplicationRole,
        string, 
        ApplicationUserClaim,
        ApplicationUserRole,
        ApplicationUserLogin,
        ApplicationRoleClaim, 
        ApplicationUserToken>
    {
        public StoreNikDbConText(DbContextOptions<StoreNikDbConText> dbContext) : base(dbContext)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductNameType> ProductNameTypes { get; set; }
        public DbSet<ProductValueType> ProductValueTypes { get; set; }
        public DbSet<PromotionDiscount> PromotionDiscounts { get; set; }
        public DbSet<ProductPromotionDiscount> ProductPromotionDiscounts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<AddressState> AddressState { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Execute config in IEntityTypeConfiguration
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
