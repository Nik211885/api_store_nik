using Application.Common;
using Application.Interface;
using ApplicationCore.Entities;
using ApplicationCore.Entities.Order;
using ApplicationCore.Entities.Products;
using ApplicationCore.Entities.Ratings;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IUser
    {
        public string? FullName { get; set; }
        public string? Image { get; set; }
        /// <summary>
        ///  RefreshToken to get new access token if access token has expires
        /// </summary>
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool? Gender { get; set; }
        //Check city has correct after save db
        [MaxLength(50)]
        public string? City { get; set; }
        /// <summary>
        /// User just have two address and address1 is required and address two don't need required
        /// </summary>
        [MaxLength(50)]
        public string? Address1 { get; set; }
        [MaxLength(50)]
        public string? Address2 { get; set; }
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<ApplicationUserClaim>? UserClaims { get; set; }
        public ICollection<ApplicationUserLogin>? UserLogins { get; set; }
        public ICollection<ApplicationUserToken>? UserTokens { get; set; }

        //Relationship to core
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<PromotionDiscount>? PromotionDiscounts { get; set; }
        public ICollection<Message>? Messages { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
