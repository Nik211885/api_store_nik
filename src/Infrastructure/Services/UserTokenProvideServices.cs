using Application.Common.ResultTypes;
using Application.Interface;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserTokenProvideServices
    {
        #region DI
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly StoreNikDbConText _dbContext;
        public UserTokenProvideServices(UserManager<ApplicationUser> userManager,
            StoreNikDbConText dbContext
            )
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        #endregion
        public async Task<IResult> VerifyTokenAsync(ApplicationUser user, string nameProviderToken, string nameToken, string token)
        {
            //Check token has exprise
            var tokenCheck = await UserTokenAsync(user.Id, nameProviderToken, nameToken);
            if(tokenCheck is null)
            {
                return FResult.Failure("You don't user service");
            }
            //Check compare token
            if(tokenCheck.Value != token)
            {
                return FResult.Failure("Invalid token");
            }
            //Check time token 
            if(tokenCheck.ExpriseToken < DateTime.UtcNow)
            {
                return FResult.Failure("Your token has exprise");
            }
            //Remove record token
            await _userManager.RemoveAuthenticationTokenAsync(user, nameProviderToken, nameToken);
            return FResult.Success();
        }

        public async Task<string> GeneratorTokenAsync(ApplicationUser user, string nameProviderToken, string nameToken)
        {
            //Generator token
            var token = RandomToken();
            //Save in db
            var userToken = await UserTokenAsync(user.Id,nameProviderToken,nameToken);
            if(userToken is null)
            {
                userToken = new ApplicationUserToken() { LoginProvider = nameProviderToken, UserId = user.Id, Name = nameToken, Value = token, ExpriseToken = DateTime.UtcNow.AddMinutes(2) };
                _dbContext.Add(userToken);
            }
            else
            {
                userToken.Value = token;
                userToken.ExpriseToken = DateTime.UtcNow.AddMinutes(2);
                _dbContext.Update(userToken);
            }
            await _dbContext.SaveChangesAsync();
            return token;
        }
        //Random 6 digital make token
        private string RandomToken()
        {
            var random = new Random();
            //If random value is 000000 => 0 toString ("D6") fill full 6 digital
            var token = random.Next(000000, 999999).ToString("D6");
            return token;
        }
        /// <summary>
        ///     Check token has exprise
        /// </summary>
        /// <returns>
        ///     Return value of token if  has still exprise
        ///     other wise null
        /// </returns>
        private async Task<ApplicationUserToken?> UserTokenAsync(string userId, string nameProviderToken, string nameToken)
        {
            var Token = from ut in _dbContext.UserTokens
                          where ut.UserId.Equals(userId)
                                  && ut.Name.Equals(nameToken)
                                  && ut.LoginProvider.Equals(nameProviderToken)
                          select ut;
            return await Token.FirstOrDefaultAsync();
        }
    }
}
