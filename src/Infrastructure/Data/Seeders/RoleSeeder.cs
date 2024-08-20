using ApplicationCore.ValueObject;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders
{
    public static class RoleSeeder
    {
        public async static Task SeederAsync(RoleManager<ApplicationRole> roleManager)
        {
            if (await roleManager.Roles.AnyAsync()) { return; }
            var roleAdmin = new ApplicationRole { Name = Role.Admin };
            var roleUser = new ApplicationRole { Name=Role.User };
            await roleManager.CreateAsync(roleAdmin);
            await roleManager.CreateAsync(roleUser);
        }
    }
}
