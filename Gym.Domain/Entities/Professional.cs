using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;

namespace Gym.Domain.Entities;

public sealed class Professional : BaseEntity
{
    public string Cref { get; set; } 
    public Guid IndividualEntityId { get; private set; }
    public IndividualEntity IndividualEntity { get; set; }

    public ICollection<Workout> Workout { get; set; }

    public Professional(string cref, Guid individualEntityId)
    {
        Validations(individualEntityId);

        IndividualEntityId = individualEntityId;
        Cref = cref;
    }

    private void Validations(Guid individualEntityId)
    {
        GlobalException.When(!individualEntityId.IsValidGuid(), "IndividualEntityId is required.");
    }
}
