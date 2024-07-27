using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.ExercisesService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.Exercises;

[Route("api/[controller]")]
//[Authorize(Policy = "AllValidUsers")]
public class ExercisesController : BaseController
{
    private readonly IExercisesService _workoutService;

    public ExercisesController(IHttpContextAccessor httpContext, 
            IExercisesService workoutService) : base(httpContext)
        => _workoutService = workoutService;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
        => GetResponse(await _workoutService.GetAll());

    [HttpGet("Get{id}")]
    public async Task<IActionResult> Get(Guid id)
        => GetResponse(await _workoutService.Get(id));

    [HttpPost("Post")]
    public async Task<IActionResult> Post([FromBody] ExerciseDTO model)
        => ApiResponse(await _workoutService.Post(model));

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] ExerciseDTO model)
        => ApiResponse(await _workoutService.Update(model));

    [HttpPatch("EnableOrDisable{id}/{status}")]
    public async Task<IActionResult> EnableOrDisable(Guid id, bool status)
        => ApiResponse(await _workoutService.EnableOrDisable(id, status));
}
