using Application.DTOs;
using Application.DTOs.Request;
using Application.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        public AccountManagerController(
            IAccountManager accountManager
            )
        {
            _accountManager = accountManager;
        }
        //After create user success redirect dashboard because user has token after regrist 
        [HttpPost("regrist")]
        public async Task<ActionResult<TokenClaimsDTO>> RegristAsync([FromBody] RegristViewModel userRegrist)
        {
            var result = await _accountManager.RegristAsync(userRegrist);
            if (result.Failure())
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.AttachedIsSuccess);
        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenClaimsDTO>> LoginAsync([FromBody] LoginViewModel userLogin)
        {
            var result = await _accountManager.LoginAsync(userLogin);
            if (result.Failure())
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.AttachedIsSuccess);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            var userId = User.Claims.First(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await _accountManager.LogoutAsync(userId);
            if (result.Success)
            {
                return Ok("Logout is success");
            }
            return BadRequest(result.Errors);
            //Refresh token is null
        }
        [HttpPost("token")]
        public async Task<ActionResult<TokenClaimsDTO>> GetTokenClaim([FromBody] TokenClaimsDTO tokenClaim)
        {
            var result = await _accountManager.GetTokenAsync(tokenClaim);
            if (result.Failure())
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.AttachedIsSuccess);
        }
        [HttpPost("sendEmailConfirmAccount")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SendEmailConfirmAccountAsync()
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var user = await _accountManager.SendConfirmEmailTokenAsync(userId);
            if (user.Success)
            {
                return NoContent();
            }
            return BadRequest(user.Errors);
        }
        [HttpPost("EmailConfirm")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EmailConfirmAsync(string token)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await _accountManager.ConfirmEmailTokenAsync(userId, token);
            if (result.Success)
            {
                return Ok("Your account has confirm email");
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("ChangePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordViewModel changePasswordModel)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await _accountManager.ChangePasswordAsync(userId, changePasswordModel);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync(string email)
        {
            var result = await _accountManager.SendEmailForgotPasswordAsync(email);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email, string token)
        {
            var result = await _accountManager.ResetPasswordAsync(email, token);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("userProfile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<UserDetailReponse?> GetDetailUserAsync()
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var userDetail = await _accountManager.GetInformationForUserAsync(userId);
            return userDetail;  
        }
        [HttpPut("UpdateProfileUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateUserDetailViewModel profile)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await _accountManager.UpdateProfileForUserAsync(userId, profile);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
