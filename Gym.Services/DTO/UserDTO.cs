namespace Gym.Services.DTO;

public class UserDTO
{
    public Guid LoginId { get; set; }
    public Guid IndividualEntityId { get; set; }
    public IndividualEntityDTO IndividualEntity { get; set; }
}
