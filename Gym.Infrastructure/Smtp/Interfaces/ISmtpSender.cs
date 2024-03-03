namespace Gym.Infrastructure.Smtp;

public interface ISmtpSender
{
    Task<bool> SendEmail(string? emailAdress = null, string? mailBody = null,
            string? title = null);
    string MailBody { get; set; }
    string Title { get; set; }
    string To { get; set; }
    bool IsBodyHtml { get; set; }
}
