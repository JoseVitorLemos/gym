using Gym.Services.DTO;

namespace Gym.Services.Authentication.TokenService;

public interface ITokenService
{
    Task<LoginResponseDTO> ResponseAuth(LoginDTO login);
    Task<LoginResponseDTO> ResponseAuth(LoginDTO login, string refreshToken);
}
