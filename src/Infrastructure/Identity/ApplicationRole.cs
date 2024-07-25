using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<ApplicationRoleClaim>? RoleClaims { get; set; }
    }
}
