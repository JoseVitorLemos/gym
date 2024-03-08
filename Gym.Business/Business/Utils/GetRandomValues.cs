using System.Text;

namespace Gym.Business.Utils;

public static class RandomHelpers
{
    public static string GenerateRandomNumbers(int numberLenght, int? start = null, int? end = null)
    {
        string randomNumbers = string.Empty;

        for (int i = 1; i <= numberLenght; i++)
            randomNumbers += Convert.ToString(new Random().Next(start ?? 0, end ?? 10));

        return randomNumbers[..numberLenght];
    }
}
