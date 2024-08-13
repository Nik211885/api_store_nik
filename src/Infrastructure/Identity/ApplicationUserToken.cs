using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public ApplicationUser? User { get; set; }
        public DateTime ExpriseToken { get; set; } = DateTime.UtcNow.AddMinutes(2);
    }
}
