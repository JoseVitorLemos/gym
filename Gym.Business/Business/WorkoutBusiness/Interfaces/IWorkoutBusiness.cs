using Gym.Domain.Entities;

namespace Gym.Business.WorkoutBusiness;

public interface IWorkoutBusiness
{
    Task<List<Workout>> ListWorkout();
    Task<Workout> GetWorkout(Guid id);
    Task<bool> InsertWorkout(Workout entity);
    Task<bool> UpdateWorkout(Workout entity);
    Task<bool> EnableOrDisableWorkout(Guid id, bool status);
}
