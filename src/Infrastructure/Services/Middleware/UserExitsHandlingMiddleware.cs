using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Services.Middleware
{
    public class UserExitsHandlingMiddleware : IMiddleware
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserExitsHandlingMiddleware(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.User.Identity is not null && context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
                var isUserExit = await _userManager.Users.Where(x => x.Id.Equals(userId)).AnyAsync();
                if (isUserExit)
                {
                    await next(context);
                }
                else
                {
                    throw new UnauthorizedAccessException("User don't exits");
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
