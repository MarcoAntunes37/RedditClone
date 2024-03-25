namespace RedditClone.Infrastructure.Service;

using Microsoft.Extensions.Options;
using RedditClone.Application.Common.Interfaces.Services;
using RedditClone.Application.Common.Errors;
using RedditClone.Infrastructure.Settings;
using System.Net;
using System.Net.Mail;

public class EmailRecovery : IEmailRecovery
{
    private readonly SmtpSettings _smtpSettings;

    public EmailRecovery(
        IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public void SendEmail(string to, string subject, string body)
    {
        using (var smtpClient = new SmtpClient(_smtpSettings.Host))
        {
            smtpClient.EnableSsl = true;
            smtpClient.Port = _smtpSettings.Port;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(
                _smtpSettings.Username, _smtpSettings.Password);
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.Username)
            };

            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (HttpCustomException)
            {
                throw new HttpCustomException(
                    httpStatusCode: HttpStatusCode.BadRequest,
                    message: "Internal server error"
                );
            }
        };
    }
}