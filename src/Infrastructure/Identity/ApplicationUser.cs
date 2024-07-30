using ApplicationCore.Entities;
using ApplicationCore.Entities.Address;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Image { get; set; }
        /// <summary>
        ///  RefreshToken to get new access token if access token has expires
        /// </summary>
        public string? RefreshToken { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool Gender { get; set; }
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<ApplicationUserClaim>? UserClaims { get; set; }
        public ICollection<ApplicationUserLogin>? UserLogins { get; set; }
        public ICollection<ApplicationUserToken>? UserTokens { get; set; }

        //Relationship to core
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<PromotionDiscount>? PromotionDiscounts { get; set; }
        public ICollection<AddressState>? AddressStates { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
