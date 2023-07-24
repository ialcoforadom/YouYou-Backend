using YouYou.Business.Models;

namespace YouYou.Business.Interfaces.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage mensagem);
    }
}
