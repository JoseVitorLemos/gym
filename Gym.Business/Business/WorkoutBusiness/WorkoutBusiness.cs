using Gym.Domain.Entities;
using Gym.Domain.Interfaces;

namespace Gym.Business.WorkoutBusiness;

public class WorkoutBusiness : IWorkoutBusiness
{
    private readonly IRepository<Workout> _workoutRepository;

    public WorkoutBusiness(IRepository<Workout> workoutRepository)
        => _workoutRepository = workoutRepository;

    public async Task<List<Workout>> ListWorkout()
        => await _workoutRepository.GetAll();

    public async Task<Workout> GetWorkout(Guid id)
        => await _workoutRepository.GetById(id);

    public async Task<bool> InsertWorkout(Workout entity)
        => await _workoutRepository.Insert(entity);

    public async Task<bool> UpdateWorkout(Workout entity)
        => await _workoutRepository.Update(entity);

    public async Task<bool> EnableOrDisableWorkout(Guid id, bool status)
        => await _workoutRepository.EnableOrDisable(id, status);
}
