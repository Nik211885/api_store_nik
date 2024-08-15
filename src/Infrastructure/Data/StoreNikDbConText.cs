using Application.Interface;
using ApplicationCore.Entities;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using Infrastructure.Identity;
using MediatR;
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
        , IStoreNikDbContext
    {
        private readonly IPublisher _publisher;
        public StoreNikDbConText(DbContextOptions<StoreNikDbConText> dbContext,
            IPublisher publisher
            ) : base(dbContext)
        {
            _publisher = publisher;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductNameType> ProductNameTypes { get; set; }
        public DbSet<ProductValueType> ProductValueTypes { get; set; }
        public DbSet<PromotionDiscount> PromotionDiscounts { get; set; }
        public DbSet<ProductPromotionDiscount> ProductPromotionDiscounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<OrderDetailProductValueType> OrderDetailProductValueType { get ; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Execute config in IEntityTypeConfiguration
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        //
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {      
            var eventBases = from entity in ChangeTracker.Entries<BaseEntity>()
                             where entity.Entity.Events is not null &&
                                   entity.Entity.Events.Any()
                             select entity;
            var result = await base.SaveChangesAsync(cancellationToken);

            foreach(var e in eventBases)
            {
                await _publisher.Publish(e, cancellationToken);
            }

            return result;

        }
    }
}
