using Gym.Domain.Enums;

namespace Gym.Presentation.Controllers;

public class ClaimsTypes
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }

    public ClaimsTypes(Guid id, string email, Roles role)
    {
        Id = id;
        Email = email;
        Role = role;
    }
}
