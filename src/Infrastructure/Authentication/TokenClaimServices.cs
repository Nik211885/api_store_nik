using Application.DTOs;
using Application.Interface;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Authentication
{
    public class TokenClaimServices : ITokenClaims
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StoreNikDbConText _dbContext;
        private readonly IConfiguration _configuration;
        public TokenClaimServices(UserManager<ApplicationUser> userManager, StoreNikDbConText dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }
        public async Task<TokenClaimsDTO> GetTokenClaimsAsync(string userId)
        {
            var accessToken = await GetJwtAccessTokenAsync(userId);
            var refreshToken = await GetRefreshToken(userId);
            return new TokenClaimsDTO(accessToken, refreshToken);
        }

        public string? GetUserIdByTokenClaim(string accessToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadToken(accessToken) as JwtSecurityToken;
            if(jwtToken is null)
            {
                return null;
            }
            var userId = jwtToken.Claims.First(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            return userId;
        }

        public bool IsAccessTokenHasExpires(string accessToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(accessToken);
            if(jwtToken is null || jwtToken.ValidTo < DateTime.UtcNow)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> IsRefreshTokenAsync(string refreshToken, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpires < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return false;
            }
            user.RefreshToken = null;
            user.RefreshTokenExpires = DateTime.MinValue;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<string> GetJwtAccessTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User is not exits");
            }
            var queryGetRoles = from userRole in _dbContext.UserRoles
                                where userRole.UserId.Equals(user.Id)
                                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                                select role.Name;
            var rolesForUser = await queryGetRoles.ToListAsync();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Id),
            };
            if(user.UserName is not null)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            }
            foreach(var r in rolesForUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }
            var key = _configuration["Jwt:Key"];
            if(key is null)
            {
                throw new Exception("Key is null");
            }
            var securityKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(key));
            var signingCredential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signingCredential,
                    claims:claims
                );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return jwtToken;
        }
        private async Task<string> GetRefreshToken(string userId)
        {
            byte[] rand = new byte[64];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(rand);
            }
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User is not null");
            //Save refresh Token into database
            string refreshToken =  Convert.ToBase64String(rand);
            DateTime exprise = DateTime.Now.AddDays(7);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = exprise;
            await _userManager.UpdateAsync(user);
            return refreshToken;
        }
    }
}
