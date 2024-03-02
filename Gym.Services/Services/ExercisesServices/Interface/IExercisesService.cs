using Gym.Services.DTO;

namespace Gym.Services.ExercisesService;

public interface IExercisesService
{
    Task<List<ExerciseDTO>> ListExercises();
    Task<ExerciseDTO> GetExercises(Guid id);
    Task<bool> InsertExercises(ExerciseDTO model);
    Task<bool> UpdateExercises(ExerciseDTO model);
    Task<bool> EnableOrDisableExercises(Guid id, bool status);
}
