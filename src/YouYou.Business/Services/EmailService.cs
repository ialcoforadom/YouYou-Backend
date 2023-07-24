using MailKit.Net.Smtp;
using MimeKit;
using YouYou.Business.Interfaces.Emails;
using YouYou.Business.Models;

namespace YouYou.Business.Services
{
    public class EmailService : IEmailService
    {

        private readonly EmailConfiguration _emailConfig;

        public EmailService(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var emailMensagem = CreateEmailMessage(message);

            await SendAsync(emailMensagem);
        }

        private MimeMessage CreateEmailMessage(EmailMessage mensagem)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(mensagem.To);
            emailMessage.Subject = mensagem.Subject;              
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mensagem.Content };

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, _emailConfig.UseSsl);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
