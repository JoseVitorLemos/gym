using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.LoginService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.User;

[Route("api/[controller]")]
[Authorize(Policy = "AllUsers")]
public class LoginController : BaseController
{
    private readonly ILoginService _loginServicer;

    public LoginController(IHttpContextAccessor httpContext, 
            ILoginService userService) : base(httpContext)
        => _loginServicer = userService;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
        => ApiResponse(await _loginServicer.Login(model), "Login with success !");

    [AllowAnonymous]
    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] LoginDTO model)
        => ApiResponse(await _loginServicer.Signup(model), "Signup with success !");

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] LoginResetPasswordDTO model)
        => ApiResponse(await _loginServicer.ResetPassword(model));

    [Authorize(Policy = "EmailConfirmation")]
    [HttpPost("ResendEmailConfirmation")]
    public async Task<IActionResult> ResendEmailConfirmation(string email)
    {
        if (ClaimsTypes.Email != email)
            return ApiResponse<object?>(null, "Invalid email provided");

        return ApiResponse(await _loginServicer.ResendEmailConfirmation(email));
    }

    [Authorize(Policy = "EmailConfirmation")]
    [HttpPost("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string codeConfirmation)
        => ApiResponse(await _loginServicer.ConfirmEmail(ClaimsTypes.Email, codeConfirmation));
}
