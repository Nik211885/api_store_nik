using Application.Common.ResultTypes;
using Application.Interface;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Services.Email
{
    public class EmailServices : IEmail
    {
        private readonly MailSettings _mailSettings;
        public EmailServices(IOptions<MailSettings> mailSettingOptions)
        {
            _mailSettings = mailSettingOptions.Value;   
        }
        public async Task<IResult> SendEmailAsync(string to, string body, string subject, string? nameTo = null, bool isLink = false)
        {
            try
            {
                var emailMessage = new MimeMessage();
                var emailForm = new MailboxAddress(_mailSettings.Name, _mailSettings.EmailId);
                emailMessage.From.Add(emailForm);
                if(nameTo is null)
                {
                    nameTo = to.Split("@")[0];
                }
                var emailTo = new MailboxAddress(nameTo, to);
                emailMessage.To.Add(emailTo);
                emailMessage.Subject = subject;
                var bodyBuilder = new BodyBuilder();
                if (!isLink)
                {
                    bodyBuilder.TextBody = body;
                }
                else
                {
                    bodyBuilder.HtmlBody = body;
                }
                emailMessage.Body = bodyBuilder.ToMessageBody();
                var mailClient = new SmtpClient();
                await mailClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, _mailSettings.UseSSL);
                await mailClient.AuthenticateAsync(_mailSettings.EmailId, _mailSettings.Password);
                await mailClient.SendAsync(emailMessage);
                await mailClient.DisconnectAsync(true);
                mailClient.Dispose();
                return FResult.Success();
            }
            catch (Exception ex)
            {
                return FResult.Failure(ex);
            }
        }
    }
}
