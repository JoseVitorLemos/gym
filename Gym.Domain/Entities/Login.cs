using Gym.Domain.Enums;
using Gym.Helpers.Validations;
using Gym.Helpers.Exceptions;
using Gym.Helpers.HashPassword;

namespace Gym.Domain.Entities;

public sealed class Login : BaseEntity
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Roles Role { get; private set; }

    public ICollection<LoginConfirmation> LoginConfirmation { get; set; }

    public Login() { }

    public Login(string email, string password, Roles? role = Roles.EmailConfirmation)
    {
        Validations(email, password, role.Value);

        Email = email;
        Password = password;
        Role = role.Value;
    }

    public void SetPassword(string password)
    {
        GlobalException.When(password?.Length < 8, "Invalid min password Length");
        GlobalException.When(password?.Length > 16, "Invalid max password Length");
        Password = BcryptAdapter.HashPassword(password);
    }

    public void SetRole(Roles role)
    {
        GlobalException.When(!EnumValidations.IsValidEnum<Roles>(role), "Invalid Roles provided");
        Role = role;
    }

    public void SetStatus(bool status)
        => Status = status;

    private void Validations(string email, string password, Roles role)
    {
        GlobalException.When(!email.IsValidEmail(), "Invalid Email");
        GlobalException.When(password?.Length < 8, "Invalid min password Length");
        GlobalException.When(password?.Length > 16, "Invalid max password Length");
        GlobalException.When(!EnumValidations.IsValidEnum<Roles>(role), "Invalid Roles provided");
    }
}
