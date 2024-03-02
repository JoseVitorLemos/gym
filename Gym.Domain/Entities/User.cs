using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;

namespace Gym.Domain.Entities;

public sealed class User : BaseEntity
{
    public Guid LoginId { get; private set; }
    public Login Login { get; set; }
    public Guid IndividualEntityId { get; private set; }
    public IndividualEntity IndividualEntity { get; set; }

    public User(Guid loginId, Guid individualEntityId)
    {
        Validations(loginId, individualEntityId);

        LoginId = loginId;
        IndividualEntityId = individualEntityId;
    }

    private void Validations(Guid loginId, Guid individualEntityId)
    {
        GlobalException.When(!GuidValidations.IsValid(loginId), "Invalid loginId provided");
        GlobalException.When(!GuidValidations.IsValid(individualEntityId), "Invalid invidividualEntityId provided");
    }
}
