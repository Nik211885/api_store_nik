using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public ApplicationRole? Role { get; set; }
    }
}
