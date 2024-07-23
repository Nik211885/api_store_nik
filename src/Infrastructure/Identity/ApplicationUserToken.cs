using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public ApplicationUser? User { get; set; }
    }
}
