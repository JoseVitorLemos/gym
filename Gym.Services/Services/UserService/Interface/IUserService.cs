using Gym.Services.DTO;

namespace Gym.Services.UserService;

public interface IUserService
{
    Task<LoginResponseDTO> Login(LoginDTO model);
    Task<bool> Signup(UserDTO model);
}
