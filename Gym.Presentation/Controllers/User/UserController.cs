using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.User;

[Route("api/[controller]")]
[Authorize(Roles = "Authenticated")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IHttpContextAccessor httpContext, IUserService userService) : base(httpContext)
        => _userService = userService;

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
        => ApiResponse(await _userService.Login(model));

    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] UserDTO model)
        => ApiResponse(await _userService.Signup(model));
}
