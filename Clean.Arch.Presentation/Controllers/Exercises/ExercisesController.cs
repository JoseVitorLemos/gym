using Microsoft.AspNetCore.Mvc;
using Clean.Arch.Services.DTO;
using Clean.Arch.Services.ExercisesService;
using Microsoft.AspNetCore.Authorization;

namespace Clean.Arch.Presentation.Controllers.Exercises;

[Route("v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,Personal")]
public class ExercisesController : BaseController
{
    private readonly IExercisesService _workoutService;

    public ExercisesController(IExercisesService workoutService)
        => _workoutService = workoutService;

    [HttpGet("ListExercises")]
    public async Task<IActionResult> ListExercises()
       => GetResponse(await _workoutService.ListExercises());

    [HttpGet("GetExercises")]
    public async Task<IActionResult> GetExercises(Guid id)
        => GetResponse(await _workoutService.GetExercises(id));

    [HttpPost("InsertExercises")]
    public async Task<IActionResult> InsertExercises([FromBody] ExerciseDTO model)
        => ApiResponse(await _workoutService.InsertExercises(model));

    [HttpPut("UpdateExercises")]
    public async Task<IActionResult> UpdateExercises([FromBody] ExerciseDTO model)
        => ApiResponse(await _workoutService.UpdateExercises(model));

    [HttpPatch("EnableOrDisableExercises")]
    public async Task<IActionResult> EnableOrDisableExercises(Guid id, bool status)
        => ApiResponse(await _workoutService.EnableOrDisableExercises(id, status));
}
