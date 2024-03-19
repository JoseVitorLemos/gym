using Gym.Services.DTO;

namespace Gym.Services.LoginService;

public interface ILoginService
{
    Task<LoginResponseDTO> Login(LoginDTO model);
    Task<LoginResponseDTO> Signup(LoginDTO model);
    Task<bool> ResetPassword(LoginResetPasswordDTO model);
    Task<bool> ResendEmailConfirmation(string email);
    Task<bool> ConfirmEmail(string email, string codeConfirmation);
    Task<LoginResponseDTO> RefreshToken(string email, string refreshToken);
}
