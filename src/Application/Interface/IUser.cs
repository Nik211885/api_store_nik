namespace Application.Interface
{
    public interface IUser
    {
        public string Id { get; set; } 
        public string? FullName { get; set; }
        public string? Image { get; set; }
        public string? Bio { get; set; }
        public DateTime? BirthDay { get; set; }
        public bool? Gender { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
    }
}
