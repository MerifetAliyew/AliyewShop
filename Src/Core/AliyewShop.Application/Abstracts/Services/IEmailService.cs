using AliyewShop.Application.Abstracts.Services;
namespace AliyewShop.Application.Abstracts.Services;


public interface IEmailService
{
    Task SendEmailAsync(IEnumerable<string> toEmails, string subject, string body);
}
