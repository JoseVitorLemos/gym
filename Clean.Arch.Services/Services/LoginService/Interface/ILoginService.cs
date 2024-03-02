using Clean.Arch.Services.DTO;

namespace Clean.Arch.Services.LoginService;

public interface ILoginService
{
    Task<LoginResponseDTO> Login(LoginDTO model);
    Task<bool> Signup(LoginDTO model);
    Task<bool> ResetPassword(LoginDTO entity);
}
