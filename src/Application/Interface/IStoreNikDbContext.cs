using ApplicationCore.Entities;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using Microsoft.EntityFrameworkCore;

namespace Application.Interface
{
    public interface IStoreNikDbContext
    {
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
        public DbSet<OrderDetailProductValueType> OrderDetailProductValueType { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
