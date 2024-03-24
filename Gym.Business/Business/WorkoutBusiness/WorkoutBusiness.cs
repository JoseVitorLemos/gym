using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Business.WorkoutBusiness;

public class WorkoutBusiness : IWorkoutBusiness
{
    private readonly IRepository<Workout> _workoutRepository;
    private readonly IRepository<Professional> _profissionalRepository;

    public WorkoutBusiness(IRepository<Workout> workoutRepository,
            IRepository<Professional> profissionalRepository)
    {
        _workoutRepository = workoutRepository;
        _profissionalRepository = profissionalRepository;
    }

    public async Task<List<Workout>> ListWorkout()
        => await _workoutRepository.GetAll();

    public async Task<Workout> GetWorkout(Guid id)
        => await _workoutRepository.GetById(id);

    public async Task<bool> InsertWorkout(Workout entity, Guid loginId)
    {
        var professional = await _profissionalRepository
            .FindByCondition(x => x.IndividualEntity.LoginId.Equals(loginId));

        if (!professional.Any())
            throw new GlobalException(HttpStatusCodes.BadRequest,
                "Only professionals can register training");

        Guid professionalId = professional.First().Id;

        if (await ExistsDivisionWorkout(entity.Division, professionalId, entity.FitnessClientId))
            throw new GlobalException(HttpStatusCodes.BadRequest,
                $"Workout division ({entity.Division}). Its already registered");

        entity.SetPersonal(professional.First().Id);

        return await _workoutRepository.Insert(entity);
    }

    private async Task<bool> ExistsDivisionWorkout(WorkoutDivision division, 
        Guid professionalId, Guid fitnessCLientId)
        => (await _workoutRepository
        .FindByCondition(x => x.Division.Equals(division) &&
                         x.PersonalId.Equals(professionalId) &&
                         x.FitnessClientId.Equals(fitnessCLientId))).Any();

    public async Task<bool> UpdateWorkout(Workout entity)
        => await _workoutRepository.Update(entity);

    public async Task<bool> EnableOrDisableWorkout(Guid id, bool status)
        => await _workoutRepository.EnableOrDisable(id, status);
}
