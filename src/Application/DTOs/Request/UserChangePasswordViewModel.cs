namespace Application.DTOs.Request
{
    public record UserChangePasswordViewModel(
        string currentPassword,
        string newPassword, 
        string confirmPassword
        ) ;
}
