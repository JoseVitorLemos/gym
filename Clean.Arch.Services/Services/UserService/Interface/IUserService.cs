using Clean.Arch.Services.DTO;

namespace Clean.Arch.Services.UserService;

public interface IUserService
{
    Task<LoginResponseDTO> Login(LoginDTO model);
    Task<bool> Signup(UserDTO model);
}
