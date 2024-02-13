namespace RedditClone.Infrastructure.Service;

using RedditClone.Application.Common.Interfaces.Services;
using System.Net;
using System.Net.Mail;

public class EmailRecovery : IEmailRecovery
{

    public void SendEmail(string to, string subject, string body)
    {
        using (var smtpClient = new SmtpClient("smtp-relay.brevo.com"))
        {
            smtpClient.EnableSsl = true;
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(
                "kgbstrike@gmail.com", "bxRX4VTKN3zDB2hE");
            var mailMessage = new MailMessage
            {
                From = new MailAddress("kgbstrike@gmail.com")
            };

            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            smtpClient.Send(mailMessage);
        };
    }
}