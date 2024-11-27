namespace BirdPage.Infrastructure.Email;

public class EmailOptions
{
    public string SenderAddress { get; set; } = null!;    
    public string RecipientAddress { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string RecipientName { get; set; } = null!;
    public string SmtpServer { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int SmtpPort { get; set; }
    
}