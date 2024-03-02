using Clean.Arch.Domain.Enums;
using Clean.Arch.Validations;

namespace Clean.Arch.Services.DTO;

public class WorkoutDTO
{
    public Guid Id { get; set; }
    [EnumAnnotations<WorkoutDivision>(ErrorMessage = "WorkoutDivision is required")]
    [GuidAnnotations(ErrorMessage = "PersonalId is required")]
    public Guid PersonalId { get; set; }
    [GuidAnnotations(ErrorMessage = "IndividualEntityId is required")]
    public Guid IndividualEntityId { get; set; }
}
