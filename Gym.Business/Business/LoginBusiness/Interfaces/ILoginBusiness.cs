using Gym.Domain.Entities;
using Gym.Domain.Enums;

namespace Gym.Business.LoginBusiness;

public interface ILoginBusiness
{
    Task<Login> Login(Login entity);
    Task<Login> Signup(Login entity);
    Task<bool> ResetPassword(Login entity, string newPassword);
    Task<bool> ResendEmailConfirmation(string email);
    Task<bool> ConfirmEmail(string email, string codeConfirmation);
    Task<Login> FindByEmail(string email);
    Task<Login> GetLoginById(Guid id);
    Task UpdateRoleFromId(Guid id, Roles role);
}
