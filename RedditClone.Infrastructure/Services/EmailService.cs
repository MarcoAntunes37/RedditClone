namespace RedditClone.Infrastructure.Services;

using ErrorOr;
using Serilog;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using RedditClone.Infrastructure.Settings;
using RedditClone.Infrastructure.Common.Errors;
using RedditClone.Application.Common.Interfaces.Services;

internal sealed class EmailService(IOptions<SmtpSettings> smtpSettings) : IEmailService
{
    private readonly SmtpSettings _smtpSettings = smtpSettings.Value;

    public async Task SendRecoveryEmailAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        SendEmail(to, subject, body);
    }

    public async Task SendWelcomeEmailAsync(string to, string username, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;

        var subject = "Welcome to Reddit clone.";
        var body = @$"Hello {username},
        Welcome to Reddit clone, we are happy are parte of us.
        We have a lot of communities and great people to interact.";

        SendEmail(to, subject, body);
    }

    public ErrorOr<bool> SendEmail(string to, string subject, string body)
    {
        try
        {
            Log.Information(
                "{@Message}",
                "Trying to send email to {@Email}",
                to);

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

                smtpClient.Send(mailMessage);

                return true;
            };
        } catch(Exception ex)
        {
            Error error = Errors.SmtpServer.EmailNotSent;

            Log.Error(
                "{@Code}, {@Description}, {@Exception}",
                error.Code,
                error.Description,
                ex);

            return error;
        }
    }
}