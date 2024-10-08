﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public ApplicationUser? User { get; set; }
    }
}
