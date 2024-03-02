using Clean.Arch.Services.DTO;

namespace Clean.Arch.Services.IndividualEntityService;

public interface IWorkoutService
{
    Task<List<WorkoutDTO>> ListWorkout();
    Task<WorkoutDTO> GetWorkout(Guid id);
    Task<bool> InsertWorkout(WorkoutDTO model);
    Task<bool> UpdateWorkout(WorkoutDTO model);
    Task<bool> EnableOrDisableWorkout(Guid id, bool status);
}
