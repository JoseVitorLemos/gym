using Clean.Arch.Domain.Entities;

namespace Clean.Arch.Business.LoginBusiness;

public interface ILoginBusiness
{
    Task<Login> Login(Login entity);
    Task<bool> Signup(Login entity);
    Task<bool> ResetPassword(Login entity);
}
