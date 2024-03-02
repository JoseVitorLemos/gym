using Microsoft.AspNetCore.Mvc;
using Clean.Arch.Services.DTO;
using Clean.Arch.Services.UserService;

namespace Clean.Arch.Presentation.Controllers.User;

[Route("v1/[controller]")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
        => _userService = userService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
        => ApiResponse(await _userService.Login(model));

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] UserDTO model)
        => ApiResponse(await _userService.Signup(model));
}
