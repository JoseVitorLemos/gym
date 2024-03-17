using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.Authentication.TokenService;

public interface ITokenService
{
    LoginResponseDTO ResponseAuth(Login login);
}
