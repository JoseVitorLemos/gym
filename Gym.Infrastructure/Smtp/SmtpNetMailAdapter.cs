using System.Configuration;
using System.Net;
using System.Net.Mail;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Infrastructure.Smtp;

public class SmtpNetMailAdapter : ISmtpSender
{
    public string MailBody { get; set; }
    public string Title { get; set; }
    public string To { get; set; }
    public bool IsBodyHtml { get; set; }

    private readonly string _emailFrom;
    private readonly string _userName;
    private readonly string _password;
    private readonly string _host;
    private readonly int _port;
    private readonly bool _enableSsl;

    public SmtpNetMailAdapter()
    {
        string emailFrom = ConfigurationManager.AppSettings["Email"];
        string userName = ConfigurationManager.AppSettings["userName"];
        string password = ConfigurationManager.AppSettings["password"];
        string host = ConfigurationManager.AppSettings["host"];
        string port = ConfigurationManager.AppSettings["port"];
        string enableSSL = ConfigurationManager.AppSettings["EnableSSL"];

        ValidationsSettings(emailFrom, userName, password, host, port, enableSSL);

        MailBody = string.Empty;
        Title = string.Empty;
        To = string.Empty;
        IsBodyHtml = true;

        _emailFrom = emailFrom;
        _userName = userName;
        _password = password;
        _host = host;
        _port = int.Parse(port);
        _enableSsl = bool.Parse(enableSSL);
    }

    public async Task<bool> SendEmail()
    {
        ValidateEmail();

        SmtpClient client = new SmtpClient(_host, _port);
        client.Credentials = new NetworkCredential(_userName, _password);
        client.EnableSsl = _enableSsl;
        client.Host = _host;
        client.Port = _port;

        MailMessage message = new MailMessage(_emailFrom,
                                              To,
                                              Title,
                                              MailBody);
        message.IsBodyHtml = IsBodyHtml;

        try
        {
            await client.SendMailAsync(message);
            return true;
        }
        catch (Exception e)
        {
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message);
        }
    }

    private void ValidationsSettings(string emailFrom, string userName, string password,
            string host, string port, string enableSSL)
    {
        GlobalException.When(string.IsNullOrEmpty(emailFrom), "Invalid SMTP email provided");
        GlobalException.When(string.IsNullOrEmpty(userName), "Invalid SMTP userName provided");
        GlobalException.When(string.IsNullOrEmpty(password), "Invalid SMTP password provided");
        GlobalException.When(string.IsNullOrEmpty(host), "Invalid SMTP host provided");
        GlobalException.When(!int.TryParse(port, out int validatePort), "Invalid SMTP port provided");
        GlobalException.When(!bool.TryParse(enableSSL, out bool validateEnableSSL), "Invalid SMTP enableSSL provided");
    }

    private void ValidateEmail()
    {
        GlobalException.When(string.IsNullOrEmpty(MailBody), "Invalid SmtpSettings MailBody");
        GlobalException.When(string.IsNullOrEmpty(Title), "Invalid SmtpSettings Title");
        GlobalException.When(string.IsNullOrEmpty(To), "Invalid SmtpSettings To");
    }
}
