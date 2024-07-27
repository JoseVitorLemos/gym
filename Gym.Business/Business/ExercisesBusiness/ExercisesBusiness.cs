using Gym.Domain.Entities;
using Gym.Domain.Interfaces;

namespace Gym.Business.ExercisesBusiness;

public class ExercisesBusiness : IExercisesBusiness
{
    private readonly IRepository<Exercise> _ExercisesRepository;

    public ExercisesBusiness(IRepository<Exercise> ExercisesRepository)
        => _ExercisesRepository = ExercisesRepository;

    public async Task<List<Exercise>> GetAll()
        => await _ExercisesRepository.GetAll();

    public async Task<Exercise> Get(Guid id)
        => await _ExercisesRepository.GetById(id);

    public async Task<bool> Post(Exercise entity)
        => await _ExercisesRepository.Insert(entity);

    public async Task<bool> Update(Exercise entity)
        => await _ExercisesRepository.Update(entity);

    public async Task<bool> EnableOrDisable(Guid id, bool status)
        => await _ExercisesRepository.EnableOrDisable(id, status);
}
