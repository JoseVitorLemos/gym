using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;

namespace Gym.Domain.Entities;

public sealed class LoginConfirmation : BaseEntity
{
    public Guid LoginId { get; private set; }
    public Login Login { get; set; }
    public int Code { get; private set; }

    public LoginConfirmation(Guid loginId, int code)
    {
        Validations(loginId, code);

        LoginId = loginId;
        Code = code;
    }

    private void Validations(Guid loginId, int code)
    {
        GlobalException.When(loginId.IsValid(), "Invalid LoginId provided");
        GlobalException.When(code.ToString().Length != 6, "Invalid Code length provided");
    }
}
