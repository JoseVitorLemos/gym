using Gym.Validations;

namespace Gym.Services.DTO;

public class ProfessionalDTO
{
    public Guid Id { get; set; }
    public string Cref { get; set; }
    [GuidAnnotations(ErrorMessage = "IndividualEntityId is required")]
    public Guid IndividualEntityId { get; set; }
}
