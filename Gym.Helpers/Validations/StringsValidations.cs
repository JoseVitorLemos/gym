using System.Globalization;
using System.Text.RegularExpressions;

namespace Gym.Helpers.Validations;

public static class StringsValidations
{
    public static bool IsValidCpf(this string cpf)
    {
        if (string.IsNullOrEmpty(cpf))
            return false;

        string cpfClear = Regex.Replace(cpf, @"[^a-zA-Z0-9]", "");

        if (cpfClear.Length != 11)
            return false;

        if (new string(cpfClear[0], cpfClear.Length) == cpfClear)
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
            sum += (10 - i) * (cpfClear[i] - '0');

        int rest = sum % 11;
        int digitValidationOne = rest < 2 ? 0 : 11 - rest;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += (11 - i) * (cpfClear[i] - '0');

        rest = sum % 11;
        int digitValidationTwo = rest < 2 ? 0 : 11 - rest;

        return digitValidationOne == (cpfClear[9] - '0') && digitValidationTwo == (cpfClear[10] - '0');
    }

    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();
                string domainName = idn.GetAscii(match.Groups[2].Value);
                return match.Groups[1].Value + domainName;
            }
        }
        catch
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public static bool IsNullOrEmpty(this string str)
        => string.IsNullOrEmpty(str);

    public static bool IsNullOrWhiteSpace(this string str)
        => string.IsNullOrWhiteSpace(str);
}
