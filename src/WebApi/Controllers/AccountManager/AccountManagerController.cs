using Application.DTOs;
using Application.Interface;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers.AccountManager
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
            if(result.Failure())
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
            var user = await _accountManager.SendEmailConfirmAsync(userId);
            if (user.Success) {
                return NoContent();
            }
            return BadRequest(user.Errors);
        }
        [HttpPost("EmailConfirm")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> EmailConfirm(string token)
        {
            var userId = User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            var result = await _accountManager.ConfirmEmailTokenAsync(userId, token);
            if (result.Success)
            {
                return Ok("Your account has confirm email");
            }
            return BadRequest(result.Errors);
        }

    }
}
