using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;

namespace Gym.Domain.Entities;

public sealed class LoginConfirmation : BaseEntity
{
    public Guid LoginId { get; private set; }
    public Login Login { get; set; }
    public string Code { get; private set; }
    public bool EmailConfirmation { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }

    public LoginConfirmation(Guid loginId, string code)
    {
        Validations(loginId, code);

        LoginId = loginId;
        Code = code;
    }

    public void SetEmailConfirmation(bool emailConfirmation)
        => EmailConfirmation = emailConfirmation;

    public void SetConfirmedDate()
    => ConfirmedAt = DateTime.UtcNow;

    private void Validations(Guid loginId, string code)
    {
        GlobalException.When(!loginId.IsValid(), "Invalid LoginId provided");
        GlobalException.When(code.Length != 6, "Invalid Code length provided");
    }
}
