using Gym.Domain.Enums;
using Gym.Services.DTO;

namespace Gym.Services.Authentication.TokenService;

public interface ITokenService
{
    string GetToken(string clainId, string clainEmail, string clainRole);
    LoginResponseDTO ResponseAuth(string id, string email, Roles role);
}
