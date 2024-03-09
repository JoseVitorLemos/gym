using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.ExercisesService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.Exercises;

[Route("api/[controller]")]
[Authorize(Policy = "AllValidUsers")]
public class ExercisesController : BaseController
{
    private readonly IExercisesService _workoutService;

    public ExercisesController(IHttpContextAccessor httpContext, 
            IExercisesService workoutService) : base(httpContext)
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
