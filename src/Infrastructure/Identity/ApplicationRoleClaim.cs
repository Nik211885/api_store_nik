using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public ApplicationRole? Role { get; set; }
    }
}
