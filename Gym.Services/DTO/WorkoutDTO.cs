using Gym.Domain.Enums;
using Gym.Validations;

namespace Gym.Services.DTO;

public class WorkoutDTO
{
    public Guid Id { get; set; }
    [EnumAnnotations<WorkoutDivision>(ErrorMessage = "WorkoutDivision is required")]
    [GuidAnnotations(ErrorMessage = "PersonalId is required")]
    public Guid PersonalId { get; set; }
    [GuidAnnotations(ErrorMessage = "IndividualEntityId is required")]
    public Guid IndividualEntityId { get; set; }
}
