using System.Net.Mail;
using System.Net;
using System.Runtime;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Infrastructure.Services;
using AliyewShop.Application.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AliyewShop.Infrastructure.Services;

public class EmailService : IEmailService
{
    private EmailSettings _emailSettings { get; }

    public EmailService(IOptions<EmailSettings> options)
    {
        _emailSettings = options.Value;
    }
    public async Task SendEmailAsync(IEnumerable<string> toEmails, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        foreach (var email in toEmails)
        {
            if (string.IsNullOrWhiteSpace(email))
                continue; 

            mail.To.Add(email);
        }

        using var smtp = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password),
            EnableSsl = true
        };

        await smtp.SendMailAsync(mail);
    }
}
