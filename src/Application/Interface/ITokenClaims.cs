using Application.DTOs;

namespace Application.Interface
{
    public interface ITokenClaims
    {
        /// <summary>
        /// Get access token jwt and refresh token 
        /// </summary>
        /// <param name="userName">
        ///    If user login success use userName query db and create claims for access token
        /// </param>
        /// <returns>
        ///     Return access token and refresh token
        /// </returns>
        Task<TokenClaimsDTO> GetTokenClaimsAsync(string userName);
        /// <summary>
        /// Check access token has expires
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>
        ///     Return true if access token has expires otherwise false
        /// </returns>
        Task<IResult> IsRefreshTokenAsync(string refreshToken, string userId);
        /// <summary>
        ///     Check access token have the correct signature
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>
        ///     Return userId if access token have the correct signature otherwise return nill
        /// </returns>
        Task<string?> ValidAccessTokenAsync(string accessToken);
        /// <summary>
        ///     Check access token have the correct signature no check lef time
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>
        ///     Return userId if access token have the correct signature otherwise return nill
        /// </returns>
        Task<string?> ValidAccessTokenHasExpriseAsync(string accessToken);
    }
}
