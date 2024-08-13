using Application.Common.ResultTypes;
using Application.DTOs;
using Application.Interface;
using ApplicationCore;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class AccountManagerServices : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly StoreNikDbConText _dbContext;
        private readonly IEmail _iEmailServices;
        private readonly ITokenClaims _tokenClaim;
        public AccountManagerServices(
            IEmail iEmailServices,
            StoreNikDbConText dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenClaims tokenClaim
            )
        {
            _iEmailServices = iEmailServices;
            _dbContext = dbContext;
            _tokenClaim = tokenClaim;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IResult> SendEmailConfirmAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return FResult.Failure("User is not exits");
            }
            if(user.Email is null)
            {
                return FResult.Failure("Please add your email after confirm account");
            }
            //Check user has confirm email
            if (user.EmailConfirmed)
            {
                return FResult.Failure("User has confirm email");
            }
            //Generate token for email user confirm
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (token is null)
            {
                return FResult.Failure("Don't create token for confirm email");
            }
            string body = $"Thank you for use our services and this code for confirm email is {token} you have two minute for confirm";
            var result = await _iEmailServices.SendEmailAsync(user.Email, body, "Verify Email For Your Account", true);
            if (result.Success)
            {
                return FResult.Success();
            }
            return FResult.Failure(result.Errors);
        }

        public async Task<IResult> GetTokenAsync(TokenClaimsDTO tokenClaim)
        {
            var userId = await _tokenClaim.ValidAccessTokenHasExpriseAsync(tokenClaim.AccessToken);
            if (userId is null)
            {
                return FResult.Failure("Access token not correct");
            }
            //Check refresh token
            var isRefreshToken = await _tokenClaim.IsRefreshTokenAsync(tokenClaim.RefreshToken, userId);
            if (isRefreshToken.Failure())
            {
                return isRefreshToken;
            }
            var userName = await _userManager.Users.
                Where(x=>x.Id.Equals(userId)).
                Select(x => x.UserName).FirstOrDefaultAsync();
            if(userName is null)
            {
                return FResult.Failure("User is not exits");
            }
            var token = await _tokenClaim.GetTokenClaimsAsync(userName);
            //Attached new token
            return FResult.Success(token);
        }

        public async Task<IResult> LoginAsync(LoginViewModel userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            if (user is null)
            {
                return FResult.Failure("User name is not correct");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, true);
            if (result.IsLockedOut)
            {
                return FResult.Failure($"User attempting  to sig in is lock out {user.LockoutEnd - DateTime.UtcNow}");
            }
            if (result.Succeeded)
            {
                //Generate jwt token
                if (user.UserName is null)
                {
                    return FResult.Failure("User is not exits");
                }
                var token = await _tokenClaim.GetTokenClaimsAsync(user.UserName);
                //Attached token
                return FResult.Success(token);
            }
            await _userManager.AccessFailedAsync(user);
            return FResult.Failure("Password is not correct");
        }

        public async Task<IResult> LogoutAsync(string userId)
        {
            if (userId is null) { return FResult.Failure("User id is not null"); }
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return FResult.Failure("User don't exits");
            }
            user.RefreshToken = null;
            user.RefreshTokenExpires = DateTime.MinValue;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
            return FResult.Success();
        }

        public async Task<IResult> RegristAsync(RegristViewModel userRegrist)
        {
            var user = new ApplicationUser
            {
                UserName = userRegrist.UserName,
                Email = userRegrist.Email,
            };
            var result = await _userManager.CreateAsync(user, userRegrist.PassWord);
            if (result.Succeeded)
            {
                //Generator token claim
                var tokenClaim = await _tokenClaim.GetTokenClaimsAsync(user.UserName);
                //Attached token claim
                return FResult.Success(tokenClaim);
            }
            return FResult.Failure(result.Errors);
        }

        public async Task<IResult> ConfirmEmailTokenAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return FResult.Failure("User is not null");
            }
            //Check token has exprise
            var loginProvider = TokenOptions.DefaultEmailProvider + nameof(Variable.Confirm);
            var isEmailTokenExprise = await IsTokenHasExpriseAsync(userId, loginProvider, loginProvider + nameof(Variable.Token));
            if (!isEmailTokenExprise)
            {
                return FResult.Failure("Token has exprise");
            }
            //Confirm token
            var confirm = await _userManager.ConfirmEmailAsync(user,token);
            if (confirm.Succeeded)
            {
                return FResult.Success();
            }
            return FResult.Failure(confirm.Errors);
            
        }
        /// <summary>
        ///     Check token has exprise
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginProvider"></param>
        /// <returns>
        ///     Return true if token has not exprise
        ///     otherwise false
        /// </returns>
        private async Task<bool> IsTokenHasExpriseAsync(string userId, string loginProvider, string name)
        {
            var isTokenHasExprise = from t in _dbContext.UserTokens
                                    where t.UserId.Equals(userId)
                                           && t.Name.Equals(name)
                                           && t.LoginProvider.Equals(loginProvider)
                                           && t.ExpriseToken > DateTime.UtcNow
                                    select t.Value;
            return await isTokenHasExprise.AnyAsync();
        }
    }
}
