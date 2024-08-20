namespace Application.DTOs.Request
{
    public record UpdateUserDetailViewModel(string? FullName, string? Image, string? Bio, DateTime? BirthDay, string? Gender, string? Email, string? PhoneNumber, string? Address1, string? Address2, string? City);
}
