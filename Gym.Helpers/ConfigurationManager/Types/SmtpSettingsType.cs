using Gym.Helpers.Exceptions;

namespace Gym.Helpers.ConfigurationManager.Types;

public class SmtpSettingsType
{
    public string DeliveryMethod { get; private set; }
    public string EmailFrom { get; private set; }
    public string Host { get; private set; }
    public int Port { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public bool EnableSSL { get; private set; }

    public SmtpSettingsType(string deliveryMethod, string emailFrom, string host,
            int port, string userName, string password, bool enableSSL)
    {
        ValidationsSettings(deliveryMethod, emailFrom, host, port, userName, password, enableSSL);

        DeliveryMethod = deliveryMethod;
        EmailFrom = emailFrom;
        Host = userName;
        Port = port;
        UserName = userName;
        Password = password;
        EnableSSL = enableSSL;
    }


    private void ValidationsSettings(string deliveryMethod, string emailFrom, string host,
            int port, string userName, string password, bool enableSSL)
    {
        GlobalException.When(string.IsNullOrEmpty(emailFrom), "Invalid SMTP email provided");
        GlobalException.When(string.IsNullOrEmpty(userName), "Invalid SMTP userName provided");
        GlobalException.When(string.IsNullOrEmpty(password), "Invalid SMTP password provided");
        GlobalException.When(string.IsNullOrEmpty(host), "Invalid SMTP host provided");
        GlobalException.When(port < 1, "Invalid SMTP port provided");
    }

}
