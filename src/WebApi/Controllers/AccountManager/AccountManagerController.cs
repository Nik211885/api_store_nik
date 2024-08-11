using Application.DTOs;
using Application.Interface;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApi.Controllers.AccountManager
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManagerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly StoreNikDbConText _dbContext;
        private readonly ITokenClaims _tokenClaim;
        public AccountManagerController(
            StoreNikDbConText dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenClaims tokenClaim
            )
        {
            _dbContext = dbContext;
            _tokenClaim = tokenClaim;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //After create user success redirect dashboard because user has token after regrist 
        [HttpPost("regrist")]
        public async Task<ActionResult<TokenClaimsDTO>> RegristAsync([FromBody] RegristViewModel userRegrist)
        {
            var user = new ApplicationUser
            {
                UserName = userRegrist.UserName,
                Email = userRegrist.Email,
            };
            var result = await _userManager.CreateAsync(user,userRegrist.PassWord);
            if (result.Succeeded)
            {
                //Generator token claim
                var tokenClaim = await _tokenClaim.GetTokenClaimsAsync(user.Id);
                return tokenClaim;
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenClaimsDTO>> LoginAsync([FromBody] LoginViewModel userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, true, true);
            if (result.Succeeded)
            {
                //Generate token claim
                //User name is not null
                var userIdQuery = from u in _dbContext.Users
                             where u.UserName!.Equals(userLogin.UserName)
                             select u.Id;
                var userId = await userIdQuery.FirstOrDefaultAsync();
                if(userId is not null)
                {
                    var tokenClaim = await _tokenClaim.GetTokenClaimsAsync(userId);
                    return tokenClaim;
                }
            }
            return BadRequest("User name or password is not correct");
        }
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            var userId = User.Claims.First(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value;
            if(userId is null)
            {
                return BadRequest("User is not exits");
            }
            var result = await  _tokenClaim.LogoutAsync(userId);
            if (result)
            {
                return NoContent();
            }
            return BadRequest();
            //Refresh token is null
        }
        [HttpPost("token")]
        public async Task<ActionResult<TokenClaimsDTO>> GetTokenClaim([FromBody] TokenClaimsDTO tokenClaim)
        {
            var accessTokenHasExprise = _tokenClaim.IsAccessTokenHasExpires(tokenClaim.AccessToken);
            if (accessTokenHasExprise)
            {
                return BadRequest("Access token still have expiry");
            }
            var userId = _tokenClaim.GetUserIdByTokenClaim(tokenClaim.AccessToken);
            if(userId is null)
            {
                return BadRequest("User is not exits");
            }
            var token = await _tokenClaim.GetTokenClaimsAsync(userId);
            return token;
        }
    }
}
