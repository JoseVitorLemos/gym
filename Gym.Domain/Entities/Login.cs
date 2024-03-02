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
    public bool EmailConfirmation { get; set; }

    public ICollection<User> User { get; set; }

    public Login() { }

    public Login(string email, string password, Roles? role = Roles.EmailConfirmation,
            bool? emailConfirmation = false)
    {
        Validations(email, password, role.Value);

        Email = email;
        Password = password;
        Role = role.Value;
        EmailConfirmation = emailConfirmation.Value;
    }

    public void SetPassword(string password)
    {
        GlobalException.When(password?.Length < 8, "Invalid min password Length");
        GlobalException.When(password?.Length > 16, "Invalid max password Length");
        Password = BcryptAdapter.HashPassword(password);
    }

    private void Validations(string email, string password, Roles role)
    {
        GlobalException.When(!email.IsValidEmail(), "Invalid Email");
        GlobalException.When(password?.Length < 8, "Invalid min password Length");
        GlobalException.When(password?.Length > 16, "Invalid max password Length");
        GlobalException.When(!EnumValidations.IsValidEnum<Roles>(role), "Invalid Roles provided");
    }
}