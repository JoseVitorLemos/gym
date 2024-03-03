namespace Gym.Business.Utils;

public static class RandomHelpers
{
    public static int GenerateRandomNumbers(int numberLenght, int? start = null, int? end = null)
    {
        string randomNumbers = string.Join("", Enumerable.Range(1, numberLenght)
            .Select(x => new Random().Next(start ?? 0, end ?? 10)).ToList());

        return Convert.ToInt32(randomNumbers[..numberLenght]);
    }
}
