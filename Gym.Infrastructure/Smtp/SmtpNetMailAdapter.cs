using System.Net;
using System.Net.Mail;
using Gym.Helpers.ConfigurationManager;
using Gym.Helpers.ConfigurationManager.Types;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Microsoft.Extensions.Configuration;

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

    public SmtpNetMailAdapter(IConfiguration configuration)
    {
        SmtpSettingsType smtpSettings = CustomConfiguration.GetSmtpSettings;

        MailBody = string.Empty;
        Title = string.Empty;
        To = string.Empty;
        IsBodyHtml = true;

        _emailFrom = smtpSettings.EmailFrom;
        _userName = smtpSettings.UserName;
        _password = smtpSettings.Password;
        _host = smtpSettings.Host;
        _port = smtpSettings.Port;
        _enableSsl = smtpSettings.EnableSSL;
    }

    private SmtpClient GetClient()
    {
        SmtpClient client = new SmtpClient(_host, _port);
        client.Credentials = new NetworkCredential(_userName, _password);
        client.EnableSsl = _enableSsl;
        client.Host = _host;
        client.Port = _port;
        return client;
    }

    public async Task<bool> SendEmail(string emailAdress = null,
            string mailBody = null, string title = null)
    {
        if (string.IsNullOrEmpty(To))
            To = emailAdress ?? string.Empty;

        if (string.IsNullOrEmpty(MailBody))
            MailBody = mailBody ?? string.Empty;

        if (string.IsNullOrEmpty(Title))
            Title = title ?? string.Empty;

        ValidateEmail();

        SmtpClient client = GetClient();

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
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message, e.InnerException);
        }
    }

    private void ValidateEmail()
    {
        GlobalException.When(string.IsNullOrEmpty(MailBody), "Invalid SmtpSettings MailBody");
        GlobalException.When(string.IsNullOrEmpty(Title), "Invalid SmtpSettings Title");
        GlobalException.When(string.IsNullOrEmpty(To), "Invalid SmtpSettings To");
    }
}
