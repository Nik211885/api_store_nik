using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public ApplicationUser? User { get; set; }
    }
}
