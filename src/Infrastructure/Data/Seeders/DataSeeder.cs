using ApplicationCore.Entities.Products;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Seeders
{
    public class DataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly StoreNikDbConText _dbContext;
        public DataSeeder(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            StoreNikDbConText dbContext
            )
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeederAsync()
        {
            await RoleSeeder.SeederAsync(_roleManager);
            await UserSeeder.SeederAsync(_userManager,_dbContext);
        }
    }
}
