using System.ComponentModel;

namespace Gym.Helpers.Utils;

public static class DateUtils
{
    public static int GetAge(this DateTime birthDate)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age)) age--;

        return age;
    }

    public static TimeSpan DifferenceNow(this DateTime oldDate)
        => DateTime.UtcNow - oldDate;

    public static string GetSecondsByDifferenceNow(this DateTime date, int waitMinutes)
    {
        TimeSpan differenceWait = TimeSpan.FromMinutes(waitMinutes) - date.DifferenceNow();
        return differenceWait.ToString(@"mm\:ss");
    }
}
