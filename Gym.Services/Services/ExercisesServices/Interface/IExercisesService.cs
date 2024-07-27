using Gym.Services.DTO;

namespace Gym.Services.ExercisesService;

public interface IExercisesService
{
    Task<List<ExerciseDTO>> GetAll();
    Task<ExerciseDTO> Get(Guid id);
    Task<bool> Post(ExerciseDTO model);
    Task<bool> Update(ExerciseDTO model);
    Task<bool> EnableOrDisable(Guid id, bool status);
}