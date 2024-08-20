using ApplicationCore.Entities.Order;
using ApplicationCore.ValueObject;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders
{
    public static class UserSeeder
    {
        public async static Task SeederAsync(UserManager<ApplicationUser> userManager, StoreNikDbConText dbContext)
        {
            if (await userManager.Users.AnyAsync()) { return; }
            var user = new ApplicationUser()
            {
                UserName = "Nik",
                Address1 = "Thôn A Xã B Huyện C Tỉnh D Thành Phố E",
                Bio = "Hello",
                BirthDay = DateTime.Parse("2003/05/18"),
                City = "City E",
                Email = "khacninh2@gmail.com",
                FullName = "Le Khac Ninh",
                Gender = true,
                Image = "../image/avatar/ninh.png",
                EmailConfirmed = true,
                PhoneNumber = "0123456789",
            };
            await userManager.CreateAsync(user,"K@lnt211885");
            await userManager.AddToRoleAsync(user, Role.User);
            dbContext.Carts.Add(new Cart(user.Id));
            await dbContext.SaveChangesAsync();
            var admin = new ApplicationUser()
            {
                UserName = "LN",
                Address1 = "Thôn A Xã B Huyện C Tỉnh D Thành Phố E",
                Bio = "Hi",
                BirthDay = DateTime.Parse("2003/08/21"),
                City = "City E",
                Email = "lt2@gmail.com",
                FullName = "LT",
                Gender = true,
                Image = "../image/avatar/lt.png",
                EmailConfirmed = true,
                PhoneNumber = "0123456789",
            };
            await userManager.CreateAsync(admin, "K@lnt211885");
            await userManager.AddToRoleAsync(admin, Role.Admin);
        }
    }
}
