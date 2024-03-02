using AutoMapper;
using Clean.Arch.Business.LoginBusiness;
using Clean.Arch.Business.WorkoutBusiness;
using Clean.Arch.Domain.Entities;
using Clean.Arch.Services.Authentication.TokenService;
using Clean.Arch.Services.DTO;

namespace Clean.Arch.Services.LoginService;

public class LoginService : ILoginService
{
    private readonly ILoginBusiness _loginBusiness;
    private readonly IUserBusiness _userBusiness;
    private readonly IMapper _mapper;
    private readonly ITokenService _authorization;

    public LoginService(ILoginBusiness loginBusiness, IMapper mapper,
            ITokenService authorization, IUserBusiness userBusiness)
    {
        _mapper = mapper;
        _loginBusiness = loginBusiness;
        _authorization = authorization;
        _userBusiness = userBusiness;
    }

    public async Task<LoginResponseDTO> Login(LoginDTO model)
    {
        var loginDto = await _loginBusiness.Login(_mapper.Map<Login>(model));
        var userDto = _mapper.Map<UserDTO>(await _userBusiness.GetUserByEmail(model.Email));

        var response = new LoginResponseDTO
        {
            Token = _authorization.GetToken(Convert.ToString(userDto?.IndividualEntityId), loginDto.Email, loginDto.Role.ToString())
        };

        return response;
    }

    public async Task<bool> Signup(LoginDTO model)
        => await _loginBusiness.Signup(_mapper.Map<Login>(model));

    public async Task<bool> ResetPassword(LoginDTO model)
        => await _loginBusiness.ResetPassword(_mapper.Map<Login>(model));
}
