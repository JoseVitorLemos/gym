namespace Clean.Arch.Infrastructure.Smtp;

public interface ISmtpSender
{
    Task<bool> SendEmail();
    string MailBody { get; set; }
    string Title { get; set; }
    string To { get; set; }
    bool IsBodyHtml { get; set; }
}
