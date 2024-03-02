using Clean.Arch.Domain.Entities;

namespace Clean.Arch.Business.WorkoutBusiness;

public interface IUserBusiness
{
    Task<List<User>> ListUser();
    Task<User> GetUser(Guid id);
    Task InsertUser(Professional entity);
    Task UpdateUser(Professional entity);
    Task EnableOrDisableUser(Guid id, bool status);
    Task<User?> GetUserByEmail(string email);
}
