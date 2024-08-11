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
        Task<TokenClaimsDTO> GetTokenClaimsAsync(string userId);
        /// <summary>
        /// Check access token has expires
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>
        ///     Return true if access token has expires otherwise false
        /// </returns>
        bool IsAccessTokenHasExpires(string accessToken);
        /// <summary>
        ///  Check refresh token in client has duplicate server 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="userId"></param>
        /// <returns>
        ///     Return true if refresh token has duplicate in server
        /// </returns>
        Task<bool> IsRefreshTokenAsync(string refreshToken, string userId);
        Task<bool> LogoutAsync(string userId);
        string? GetUserIdByTokenClaim(string accessToken);
    }
}
