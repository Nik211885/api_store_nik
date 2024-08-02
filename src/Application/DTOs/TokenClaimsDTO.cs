namespace Application.DTOs
{
    public record TokenClaimsDTO(string AccessToken, RefreshTokenDTO RefreshToken);
    public record RefreshTokenDTO(string RefreshToken, DateTime ExpiresRefreshToken);
}
