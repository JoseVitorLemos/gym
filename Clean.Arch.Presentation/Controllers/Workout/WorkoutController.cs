using Microsoft.AspNetCore.Mvc;
using Clean.Arch.Services.DTO;
using Clean.Arch.Services.IndividualEntityService;
using Microsoft.AspNetCore.Authorization;

namespace Clean.Arch.Presentation.Controllers.Workout;

[Route("v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Personal")]
public class WorkoutController : BaseController
{
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
        => _workoutService = workoutService;

    [HttpGet("ListWorkout")]
    public async Task<IActionResult> ListWorkout()
        => GetResponse(await _workoutService.ListWorkout());

    [HttpGet("GetWorkout")]
    public async Task<IActionResult> GetWorkout(Guid id)
        => GetResponse(await _workoutService.GetWorkout(id));

    [HttpPost("InsertWorkout")]
    public async Task<IActionResult> InsertWorkout([FromBody] WorkoutDTO model)
        => ApiResponse(await _workoutService.InsertWorkout(model));

    [HttpPut("UpdateWorkout")]
    public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutDTO model)
        => ApiResponse(await _workoutService.UpdateWorkout(model));

    [HttpPatch("EnableOrDisableWorkout")]
    public async Task<IActionResult> EnableOrDisableWorkout(Guid id, bool status)
        => ApiResponse(await _workoutService.EnableOrDisableWorkout(id, status));
}
