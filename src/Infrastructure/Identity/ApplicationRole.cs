﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole>? UserRoles { get; set; }
        public ICollection<ApplicationRoleClaim>? RoleClaims { get; set; }
    }
}
