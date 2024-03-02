using System.Text.RegularExpressions;

namespace Clean.Arch.Helpers.Utils;

public static class RegexHelpers
{
    public static string StringClean(string value = "", bool? words = true, bool? numerics = true, 
                                     bool? specialCharacters = true, bool? htmlTags = true)
    {
        string valueClean = Convert.ToString(value);

        if (words.HasValue && words.Value)
            valueClean = Regex.Replace(valueClean, @"[a-zA-Z]", "");

        if (numerics.HasValue && numerics.Value)
            valueClean = Regex.Replace(valueClean, @"\d", "");

        if (specialCharacters.HasValue && specialCharacters.Value)
            valueClean = Regex.Replace(valueClean, @"[^\w\s]", "");

        if (htmlTags.HasValue && htmlTags.Value)
            valueClean = Regex.Replace(valueClean, @"<.*?>", "");

        return valueClean;
    }
}
