using Gym.Services.DTO;

namespace Gym.Services.LoginService;

public interface ILoginService
{
    Task<LoginResponseDTO> Login(LoginDTO model);
    Task<bool> Signup(LoginDTO model);
    Task<bool> ResetPassword(LoginDTO entity);
}
