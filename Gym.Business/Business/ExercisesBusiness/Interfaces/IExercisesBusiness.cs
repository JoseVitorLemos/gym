using Gym.Domain.Entities;

namespace Gym.Business.ExercisesBusiness;

public interface IExercisesBusiness
{
    Task<List<Exercise>> GetAll();
    Task<Exercise> Get(Guid id);
    Task<bool> Post(Exercise entity);
    Task<bool> Update(Exercise entity);
    Task<bool> EnableOrDisable(Guid id, bool status);
}