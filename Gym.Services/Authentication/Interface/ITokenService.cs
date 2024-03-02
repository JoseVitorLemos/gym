namespace Gym.Services.Authentication.TokenService;

public interface ITokenService
{
    string GetToken(string clainId, string clainEmail, string clainRole);
}
