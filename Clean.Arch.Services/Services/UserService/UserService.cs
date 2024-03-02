using AutoMapper;
using Clean.Arch.Business.WorkoutBusiness;
using Clean.Arch.Services.Authentication.TokenService;
using Clean.Arch.Services.DTO;
using Clean.Arch.Services.UserService;

namespace Clean.Arch.Services.LoginService;

public class UserService : IUserService
{
    private readonly IUserBusiness _userBusiness;
    private readonly IMapper _mapper;
    private readonly ITokenService _authorization;

    public UserService(IUserBusiness userBusiness, IMapper mapper,
            ITokenService authorization)
    {
        _userBusiness = userBusiness;
        _mapper = mapper;
        _authorization = authorization;
    }

    public Task<LoginResponseDTO> Login(LoginDTO model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Signup(UserDTO model)
    {
        throw new NotImplementedException();
    }
}
