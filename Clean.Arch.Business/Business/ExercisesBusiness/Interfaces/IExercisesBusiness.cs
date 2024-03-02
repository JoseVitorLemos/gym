using Clean.Arch.Domain.Entities;

namespace Clean.Arch.Business.ExercisesBusiness;

public interface IExercisesBusiness
{
    Task<List<Exercise>> ListExercises();
    Task<Exercise> GetExercises(Guid id);
    Task<bool> InsertExercises(Exercise entity);
    Task<bool> UpdateExercises(Exercise entity);
    Task<bool> EnableOrDisableExercises(Guid id, bool status);
}
