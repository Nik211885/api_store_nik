namespace Application.Interface
{
    public interface IEmail
    {
        Task<IResult> SendEmailAsync(string to, string body, string subject, string? nameTo = null, bool isLink = false);
    }
}
