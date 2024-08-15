using Application.Common.ResultTypes;
using Application.DTOs;
using Application.DTOs.Request;
using Application.Interface;
using Application.Mappings;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PasswordGenerator;
using v = ApplicationCore.Variable;

namespace Infrastructure.Services
{
    public class AccountManagerServices : IAccountManager
    {
        #region DI
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly StoreNikDbConText _dbContext;
        private readonly IEmail _iEmailServices;
        private readonly ITokenClaims _tokenClaim;
        private readonly UserTokenProvideServices _userTokenProvideServices;
        public AccountManagerServices(
            IEmail iEmailServices,
            UserTokenProvideServices userTokenProvider,
            StoreNikDbConText dbContext,
            UserTokenProvideServices userTokenProvideServices,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenClaims tokenClaim
            )
        {
            _userTokenProvideServices = userTokenProvider;
            _iEmailServices = iEmailServices;
            _dbContext = dbContext;
            _tokenClaim = tokenClaim;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion
        #region Send Email To Confirm Email
        public async Task<IResult> SendConfirmEmailTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return FResult.Failure("User is not null");
            }
            //Check account has email address
            if(user.Email is null)
            {
                return FResult.Failure("Your account don't add email address");
            }
            //Check email address is confirm
            if (user.EmailConfirmed)
            {
                return FResult.Failure("Your email address has confirm");
            }
            //Generator token
            var token = await _userTokenProvideServices.GeneratorTokenAsync(user,nameof(v.ConfirmEmail),nameof(v.ConfirmEmailToken));
            string body = $"Thank you for use our services and this code for confirm email is {token} you have two minute for confirm";
            var result = await _iEmailServices.SendEmailAsync(user.Email, body, "Verify Email For Your Account", user.UserName);
            if (result.Success)
            {
                return FResult.Success();
            }
            return FResult.Failure(result.Errors);
        }
        #endregion
        #region Get Token Claim If Access token has exprise
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
        #endregion
        #region Login And Return Token
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
        #endregion
        #region Logout Recall Refresh Token
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
        #endregion
        #region Regrist new account if success return new token
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
        #endregion
        #region Verify Token for Confirm Email 
        public async Task<IResult> ConfirmEmailTokenAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return FResult.Failure("User is not null");
            }
            if (user.EmailConfirmed)
            {
                return FResult.Failure("Your email has confirm");
            }
            var result = await _userTokenProvideServices.VerifyTokenAsync(user, nameof(v.ConfirmEmail),nameof(v.ConfirmEmailToken), token);
            if (result.Success)
            {
                //Update email confirm
                user.EmailConfirmed = true;
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
                return FResult.Success();
            }
            return FResult.Failure(result.Errors);
        }
        #endregion
        #region Change your password
        public async Task<IResult> ChangePasswordAsync(string userId, UserChangePasswordViewModel userChangePassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return FResult.Failure("User is not null");
            }
            //Check new password with confirm password
            if(userChangePassword.currentPassword == userChangePassword.confirmPassword)
            {
                return FResult.Failure("New password don't like confirm password");
            }
            var result = await _userManager.ChangePasswordAsync(user, userChangePassword.currentPassword, userChangePassword.newPassword);
            //Some feature send email confirm
            if (result.Succeeded)
            {
                return FResult.Success();
            }
            return FResult.Failure(result.Errors);

        }
        #endregion
        #region Send Email Requerid user forgot password
        public async Task<IResult> SendEmailForgotPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user is null)
            {
                return FResult.Failure("Don't have account use this email");
            }
            //Generator token
            if (!user.EmailConfirmed)
            {
                return FResult.Failure("Your email address not yet confirm");
            }
            var token = await _userTokenProvideServices.GeneratorTokenAsync(user, nameof(v.ForgotPassword), nameof(v.ForgotPasswordToken));
            //Send token for user
            var body = $"Hello {user.UserName} we received required you forgot password if this is you input token {token} make update new password ";
            var subject = "Forgot Your Password";
            var result = await _iEmailServices.SendEmailAsync(email, body, subject, user.UserName);
            if (result.Success)
            {
                return FResult.Success();
            }
            return FResult.Failure(result.Errors);
        }
        #endregion
        #region Reset password and send new password for user
        public async Task<IResult> ResetPasswordAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return FResult.Failure("Don't have account use this email");
            }
            if (!user.EmailConfirmed)
            {
                return FResult.Failure("Don't support reset password when account not yet confirm password");
            }
            var isToken = await _userTokenProvideServices.VerifyTokenAsync(user, nameof(v.ForgotPassword), nameof(v.ForgotPasswordToken), token);
            if (isToken.Failure())
            {
                return FResult.Failure(isToken.Errors);
            }
            //Random new password
            var randomPassword = new Password(true,true,true,true,8);
            var passwordRandom = randomPassword.Next();
            //Save new password in db
            var hasPassword = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasPassword.HashPassword(user,passwordRandom);
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            //Send email
            var subject = "Reset your password";
            var body = $"Hello {user.UserName} your password after reset is {passwordRandom} make is secrets if you want to change password. Please login again and change your password. Thank you very much";
            var resultSendEmail = await _iEmailServices.SendEmailAsync(email, body, subject, user.UserName);
            if(resultSendEmail.Success)
            {
                return FResult.Success();
            }
            return FResult.Failure(resultSendEmail.Errors);
        }
        #endregion
        #region Update profile for user
        public async Task<IResult> UpdateProfileForUserAsync(string userId, UpdateProfileUserViewModel profile)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null)
            {
                return FResult.Failure("Don't have user");
            }
            //Map for user
            {
                if(!profile.FullName.IsNullOrEmpty())
                {
                    user.FullName = profile.FullName;
                }
                if (!profile.Image.IsNullOrEmpty())
                {
                    user.Image = profile.Image;
                }
                if (!profile.Bio.IsNullOrEmpty())
                {
                    user.Bio = profile.Bio;
                }
                if (profile.BirthDay is not null)
                {
                    user.BirthDay = profile.BirthDay;
                }
                if (profile.Gender is not null)
                {
                    user.Gender = (bool)profile.Gender;
                }
                if (!profile.Address1.IsNullOrEmpty())
                {
                    user.Address1 = profile.Address1;
                }
                if (!profile.Address2.IsNullOrEmpty())
                {
                    user.Address2 = profile.Address2;
                }
                if (!profile.City.IsNullOrEmpty())
                {
                    user.City = profile.City;
                }

            }
            //Save in db
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return FResult.Success();
        }
        #endregion  
    }
}
