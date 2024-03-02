using Gym.Services.DTO;

namespace Gym.Services.ImageExerciseService;

public interface IImageExerciseService
{
    Task<List<ImageExerciseDTO>> ListImageExercise();
    Task<ImageExerciseDTO> GetImageExercise(Guid id);
    Task InsertImageExercise(ImageExerciseDTO model);
    Task UpdateImageExercise(ImageExerciseDTO model);
    Task EnableOrDisableImageExercise(Guid id, bool status);
}
