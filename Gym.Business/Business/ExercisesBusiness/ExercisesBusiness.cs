using Gym.Domain.Entities;
using Gym.Domain.Interfaces;

namespace Gym.Business.ExercisesBusiness;

public class ExercisesBusiness : IExercisesBusiness
{
    private readonly IRepository<Exercise> _ExercisesRepository;

    public ExercisesBusiness(IRepository<Exercise> ExercisesRepository)
        => _ExercisesRepository = ExercisesRepository;

    public async Task<List<Exercise>> ListExercises()
        => await _ExercisesRepository.GetAll();

    public async Task<Exercise> GetExercises(Guid id)
        => await _ExercisesRepository.GetById(id);

    public async Task<bool> InsertExercises(Exercise entity)
        => await _ExercisesRepository.Insert(entity);

    public async Task<bool> UpdateExercises(Exercise entity)
        => await _ExercisesRepository.Update(entity);

    public async Task<bool> EnableOrDisableExercises(Guid id, bool status)
        => await _ExercisesRepository.EnableOrDisable(id, status);
}
