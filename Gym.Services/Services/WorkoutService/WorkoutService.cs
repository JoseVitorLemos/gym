using AutoMapper;
using Gym.Business.WorkoutBusiness;
using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.IndividualEntityService;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutBusiness _workoutBusiness;
    private readonly IMapper _mapper;

    public WorkoutService(IWorkoutBusiness workoutBusiness, IMapper mapper)
    {
        _workoutBusiness = workoutBusiness;
        _mapper = mapper;
    }

    public async Task<List<WorkoutDTO>> ListWorkout()
        => _mapper.Map<List<WorkoutDTO>>(await _workoutBusiness.ListWorkout());

    public async Task<WorkoutDTO> GetWorkout(Guid id)
        => _mapper.Map<WorkoutDTO>(await _workoutBusiness.GetWorkout(id));

    public async Task<bool> InsertWorkout(WorkoutDTO entity, Guid loginId)
        => await _workoutBusiness.InsertWorkout(_mapper.Map<Workout>(entity), loginId);

    public async Task<bool> UpdateWorkout(WorkoutDTO entity)
        => await _workoutBusiness.UpdateWorkout(_mapper.Map<Workout>(entity));

    public async Task<bool> EnableOrDisableWorkout(Guid id, bool status)
        => await _workoutBusiness.EnableOrDisableWorkout(id, status);
}
