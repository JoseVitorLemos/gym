using Gym.Domain.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;
using Gym.Helpers.Validations;

namespace Gym.Domain.Entities;

public sealed class Workout : BaseEntity
{
    public WorkoutDivision Division { get; private set; }
    public Guid PersonalId { get; private set; }
    public Professional Personal { get; set; }
    public Guid IndividualEntityId { get; private set; }
    public IndividualEntity IndividualEntity { get; set; }

    public ICollection<Exercise> Exercises { get; set; }

    public Workout(WorkoutDivision division, Guid individualEntityId, Guid personalId)
    {
        Validations(division, individualEntityId, personalId);

        Division = division;
        IndividualEntityId = individualEntityId;
        PersonalId = personalId;
    }

    private void Validations(WorkoutDivision division, Guid individualEntityId, Guid personalId)
    {
        GlobalException.When(!EnumValidations.IsValidEnum<WorkoutDivision>(division), "Workout division is required.");
        GlobalException.When(!GuidValidations.IsValidGuid(individualEntityId), "IndividualEntityId is required.");
        GlobalException.When(!GuidValidations.IsValidGuid(personalId), "PersonalId is required.");
    }
}
