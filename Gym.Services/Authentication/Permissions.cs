using Gym.Domain.Enums;

namespace Gym.Services.Authentication.TokenService;

public static class Permissions
{
    private static string _admin => Roles.Admin.ToString();
    private static string _emailConfirmation => Roles.EmailConfirmation.ToString();
    private static string _authorization => Roles.Authenticated.ToString();
    private static string _personal => Roles.Personal.ToString();
    private static string _fitnessClient => Roles.FitnessClient.ToString();

    public static string Personal
        => string.Format("{0},{1}", _admin, _personal);

    public static string EmailConfirmation
        => string.Format("{0},{1}", _admin, _emailConfirmation);

    public static string Authenticated 
    => string.Format("{0},{1}", _admin, _authorization);

    public static string emailconfirmationandauthorized
        => string.Format("{0},{1},{2}", _admin, _emailConfirmation, _authorization);
}
