using Gym.Domain.Entities;

namespace Gym.Business.LoginBusiness;

public interface ILoginBusiness
{
    Task<Login> Login(Login entity);
    Task<Login> Signup(Login entity);
    Task<bool> ResetPassword(Login entity, string newPassword);
    Task<bool> ResendEmailConfirmation(string email);
}