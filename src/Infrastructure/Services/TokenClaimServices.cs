using Application.Common.ResultTypes;
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

namespace Infrastructure.Services
{
    public class TokenClaimServices : ITokenClaims
    {
        #region DI
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StoreNikDbConText _dbContext;
        private readonly TokenValidationParameters _validationParameters;
        private readonly IConfiguration _configuration;
        public TokenClaimServices(
            UserManager<ApplicationUser> userManager, 
            StoreNikDbConText dbContext, 
            IConfiguration configuration,
            TokenValidationParameters validationParameters
            )
        {
            _validationParameters = validationParameters;
            _userManager = userManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }
        #endregion
        #region Get token claim
        public async Task<TokenClaimsDTO> GetTokenClaimsAsync(string userName)
        {
            var accessToken = await GetJwtAccessTokenAsync(userName);
            var refreshToken = await GetRefreshToken(userName);
            return new TokenClaimsDTO(accessToken, refreshToken);
        }
        #endregion
        #region Check Refresh token has correct

        public async Task<IResult> IsRefreshTokenAsync(string refreshToken, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpires < DateTime.UtcNow)
            {
                return FResult.Failure("Refresh token is invalid");
            }
            return FResult.Success();
        }
        #endregion
        #region Check Access Token has correct signature
        public async Task<string?> ValidAccessTokenAsync(string accessToken)
        {
            var result = await new JwtSecurityTokenHandler().ValidateTokenAsync(accessToken, _validationParameters);
            if (result.IsValid)
            {
                return result.Claims.First(x=>x.Key.Equals(ClaimTypes.NameIdentifier)).Value.ToString();
            }
            return null;
        }
        #endregion
        #region Check Token has correct signature dont check left time support allocation new token when acccess token has exprise
        public async Task<string?> ValidAccessTokenHasExpriseAsync(string accessToken)
        {
            var tokenValidationParamenter = new TokenValidationParameters()
            {
                ValidateAudience = _validationParameters.ValidateAudience,
                ValidateIssuer = _validationParameters.ValidateIssuer,
                ValidateIssuerSigningKey = _validationParameters.ValidateIssuerSigningKey,
                ValidateLifetime = false,
                IssuerSigningKey = _validationParameters.IssuerSigningKey,
                ClockSkew = _validationParameters.ClockSkew,
            };
            var result = await new JwtSecurityTokenHandler().ValidateTokenAsync(accessToken, tokenValidationParamenter);
            if (result.IsValid)
            {
                return result.Claims.First(x => x.Key.Equals(ClaimTypes.NameIdentifier)).Value.ToString();
            }
            return null;
        }
        #endregion
        #region Get Acccess token
        private async Task<string> GetJwtAccessTokenAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
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
            if (user.UserName is not null)
            {
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            }
            foreach (var r in rolesForUser)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }
            var key = _configuration["Jwt:Key"];
            if (key is null)
            {
                throw new Exception("Key is null");
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: signingCredential,
                    expires: DateTime.UtcNow.AddHours(1)
                );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return jwtToken;
        }
        #endregion
        #region Get Refresh Token
        private async Task<string> GetRefreshToken(string userName)
        {
            byte[] rand = new byte[64];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(rand);
            }
            var user = await _userManager.FindByNameAsync(userName) ?? throw new Exception("User is not null");
            //Save refresh Token into database
            string refreshToken = Convert.ToBase64String(rand);
            DateTime exprise = DateTime.Now.AddDays(7);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = exprise;
            await _userManager.UpdateAsync(user);
            return refreshToken;
        }
        #endregion
    }
}
