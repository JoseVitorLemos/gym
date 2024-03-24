using AutoMapper;
using Gym.Business.LoginBusiness;
using Gym.Domain.Entities;
using Gym.Services.Authentication.TokenService;
using Gym.Services.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Services.LoginService;

public class LoginService : ILoginService
{
    private readonly ILoginBusiness _loginBusiness;
    private readonly IMapper _mapper;
    private readonly ITokenService _authorization;

    public LoginService(ILoginBusiness loginBusiness, IMapper mapper,
            ITokenService authorization)
    {
        _mapper = mapper;
        _loginBusiness = loginBusiness;
        _authorization = authorization;
    }

    public async Task<LoginResponseDTO> Login(LoginDTO model)
    {
        var login = await _loginBusiness.Login(_mapper.Map<Login>(model));
        return await _authorization
               .ResponseAuth(_mapper.Map<LoginDTO>(login));
    }

    [AllowAnonymous]
    public async Task<LoginResponseDTO> Signup(LoginDTO model)
    {
        var login = await _loginBusiness.Signup(_mapper.Map<Login>(model));
        return await _authorization
               .ResponseAuth(_mapper.Map<LoginDTO>(login));
    }

    public async Task<bool> ResetPassword(LoginResetPasswordDTO model)
        => await _loginBusiness.ResetPassword(_mapper.Map<Login>(model), model.NewPassword);

    public async Task<bool> ResendEmailConfirmation(string email)
        => await _loginBusiness.ResendEmailConfirmation(email);


    public async Task<LoginResponseDTO> ConfirmEmail(string email, string codeConfirmation)
    {
        bool confirmated = await _loginBusiness.ConfirmEmail(email, codeConfirmation);
        if (confirmated)
        {
            var login = _mapper.Map<LoginDTO>(await _loginBusiness.FindByEmail(email));
            return await _authorization.ResponseAuth(login);
        }

        return default;
    }

    public async Task<LoginResponseDTO> RefreshToken(string email, string refreshToken)
    {
        var login = _mapper.Map<LoginDTO>(await _loginBusiness.FindByEmail(email));
        return await _authorization.ResponseAuth(login, refreshToken);
    }
}
