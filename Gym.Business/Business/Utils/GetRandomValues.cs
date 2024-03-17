namespace Gym.Business.Utils;

public static class RandomHelpers
{
    public static string GenerateRandom(int numberLenght, int? start = null,
            int? end = null, bool? numbers = true, bool? characters = true,
            bool? specialCharacters = false)
    {
        string randomNumbers = string.Empty;

        for (int i = 1; i <= numberLenght; i++)
        {
            if (numbers.HasValue && numbers.Value)
                randomNumbers += GenerateRandomNumber(start ?? 0, end ?? 10);

            if (characters.HasValue && characters.Value)
                randomNumbers += GenerateRandomCharacters(start ?? 0, end ?? 10);

            if (specialCharacters.HasValue && specialCharacters.Value)
                randomNumbers += GenerateRandomSpecialsCharacters();
        }

        return randomNumbers[..numberLenght];
    }

    private static string GenerateRandomNumber(int start, int end)
        => Convert.ToString(new Random().Next(start, end));

    private static string GenerateRandomCharacters(int start, int end)
        => Convert.ToString((char)('A' + new Random().Next(0, 26)));

    private static string GenerateRandomSpecialsCharacters()
    {
        string characters = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        int randomIndex = new Random().Next(0, characters.Length);
        return Convert.ToString(characters[randomIndex]);
    }
}
