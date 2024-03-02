using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.LoginService;

namespace Gym.Presentation.Controllers.User;

[Route("v1/[controller]")]
[ApiController]
public class LoginController : BaseController
{
    private readonly ILoginService _userService;

    public LoginController(ILoginService userService)
        => _userService = userService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
        => ApiResponse(await _userService.Login(model), "Login with success !");

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] LoginDTO model)
        => ApiResponse(await _userService.Signup(model));

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] LoginDTO model)
        => ApiResponse(await _userService.ResetPassword(model));
}
