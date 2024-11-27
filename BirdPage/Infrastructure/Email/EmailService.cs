using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BirdPage.Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options)
{
    public void SendEmail(string email, string message)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(options.Value.SenderName, options.Value.SenderAddress));
        mailMessage.To.Add(new MailboxAddress(options.Value.RecipientName, options.Value.RecipientAddress));
        mailMessage.Subject = "Sent via BirdPage Website";
        mailMessage.Body = new TextPart("plain")
        {
            Text = message + "\nSent from: " + email
        };

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Connect(options.Value.SmtpServer, options.Value.SmtpPort, true);
            smtpClient.Authenticate(options.Value.SenderAddress, options.Value.Password);
            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);
        }
    }
}