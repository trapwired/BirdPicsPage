using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MudBlazor;

namespace BirdPage.Infrastructure.Email;

public class EmailService(IOptions<EmailOptions> options)
{
    public async void SendEmail(string email, string message, ISnackbar snackbar)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(options.Value.SenderName, options.Value.SenderAddress));
        mailMessage.To.Add(new MailboxAddress(options.Value.RecipientName, options.Value.RecipientAddress));
        mailMessage.Subject = "Sent via BirdPage Website";
        mailMessage.Body = new TextPart("plain")
        {
            Text = message + "\nSent from: " + email
        };

        var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(options.Value.SmtpServer, options.Value.SmtpPort, true);
        await smtpClient.AuthenticateAsync(options.Value.SenderAddress, options.Value.Password);
        await smtpClient.SendAsync(mailMessage);
        snackbar.Add("Email sent successfully", Severity.Success);
        await smtpClient.DisconnectAsync(true);
    }
}