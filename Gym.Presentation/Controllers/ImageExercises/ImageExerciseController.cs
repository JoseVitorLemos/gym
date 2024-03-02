using Microsoft.AspNetCore.Mvc;
using Gym.Services.DTO;
using Gym.Services.ImageExerciseService;
using Microsoft.AspNetCore.Authorization;

namespace Gym.Presentation.Controllers.ImageExercise;

[Route("v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ImageExerciseController : BaseController
{
    private readonly IImageExerciseService _workoutService;

    public ImageExerciseController(IImageExerciseService workoutService)
        => _workoutService = workoutService;

    [HttpGet("ListImageExercise")]
    public async Task<IActionResult> ListImageExercise()
        => GetResponse(await _workoutService.ListImageExercise());

    [HttpGet("GetImageExercise")]
    public async Task<IActionResult> GetImageExercise(Guid id)
        => GetResponse(await _workoutService.GetImageExercise(id));

    [HttpPost("InsertImageExercise")]
    [RequestSizeLimit(5 * 1024 * 1024)]
    public async Task InsertImageExercise([FromForm] string exerciseName, IFormFile file)
    {
        var model = new ImageExerciseDTO();

        using (MemoryStream memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            model.FileName = file.FileName.Split('.').FirstOrDefault();
            model.ExerciseName = exerciseName;
            model.FileByte = Convert.ToBase64String(memoryStream.ToArray());
            model.FileType = file.FileName.Split('.').LastOrDefault();
        }

        await _workoutService.InsertImageExercise(model);
    }

    [HttpPut("UpdateImageExercise")]
    public async Task UpdateImageExercise([FromBody] ImageExerciseDTO model)
        => await _workoutService.UpdateImageExercise(model);

    [HttpPatch("EnableOrDisableImageExercise")]
    public async Task EnableOrDisableImageExercise(Guid id, bool status)
        => await _workoutService.EnableOrDisableImageExercise(id, status);
}
