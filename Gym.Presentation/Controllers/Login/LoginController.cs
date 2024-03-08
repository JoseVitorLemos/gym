using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.LoginService;
using Microsoft.AspNetCore.Authorization;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Presentation.Controllers.User;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "AllUsers")]
public class LoginController : BaseController
{
    private readonly ILoginService _userService;

    public LoginController(ILoginService userService)
        => _userService = userService;

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
        => ApiResponse(await _userService.Login(model), "Login with success !");

    [AllowAnonymous]
    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] LoginDTO model)
        => ApiResponse(await _userService.Signup(model), "Signup with success !");

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] LoginResetPasswordDTO model)
        => ApiResponse(await _userService.ResetPassword(model));

    [HttpPost("ResendEmailConfirmation")]
    public async Task<IActionResult> ResendEmailConfirmation(string email)
    {
        string? tokenEmail = User?.Claims.FirstOrDefault(x => x.Type == "Email")?.Value;
        if (tokenEmail != email)
            return ApiResponse<object?>(null, "Invalid email provided");

        return ApiResponse(await _userService.ResendEmailConfirmation(email));
    }
}
