

using ecommerce.Application.Interfaces;
using ecommerce.Application.options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ecommerce.Application.Services;

public class EmailService : IEmailService
{
    
    private readonly EmailSettings _emailSettings;
    
    public EmailService(IOptionsSnapshot<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    
    public  async Task<bool> send(string to, string subject, string message)
    {
        var ms = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_emailSettings.FromEmail),
            Subject = subject

        };
        ms.To.Add(MailboxAddress.Parse(to));
        BodyBuilder bodyBuilder = new BodyBuilder
        {
            HtmlBody = message
        };
        ms.Body = bodyBuilder.ToMessageBody();
        ms.From.Add(new MailboxAddress("your ecommerce app", _emailSettings.FromEmail));
        using (SmtpClient smtpClient = new SmtpClient())
        {
            try
            {
                smtpClient.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtpClient.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                await smtpClient.SendAsync(ms);
                smtpClient.Disconnect(true);
                return true;
            }
            catch
            {
                return false;
            }
        }
                
    }
}