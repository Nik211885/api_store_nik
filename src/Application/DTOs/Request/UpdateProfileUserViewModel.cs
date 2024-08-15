namespace Application.DTOs.Request
{
    public record UpdateProfileUserViewModel(string? FullName, string? Image, string? Bio, DateTime? BirthDay, bool? Gender, string? Address1, string? Address2, string? City);
   
}
