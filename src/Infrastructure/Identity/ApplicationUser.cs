using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<ApplicationUserClaim>? UserClaims { get; set; }
        public ICollection<ApplicationUserLogin>? UserLogins { get; set; }
        public ICollection<ApplicationUserToken>? UserTokens { get; set; }
    }
}
