namespace Application.Interface
{
    public interface IEmail
    {
        Task<IResult> SendEmailAsync(string to, string body, string subject, bool isLink = false);
    }
}
