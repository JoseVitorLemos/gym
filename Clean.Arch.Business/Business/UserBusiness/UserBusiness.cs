using Clean.Arch.Business.WorkoutBusiness;
using Clean.Arch.Domain.Entities;
using Clean.Arch.Domain.Interfaces;

namespace Clean.Arch.Business.UserBusiness;

public class UserBusiness : IUserBusiness
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<IndividualEntity> _individualEntity;

    public UserBusiness(IRepository<User> userRepository,
                        IRepository<IndividualEntity> individualEntit)
    {
        _userRepository = userRepository;
        _individualEntity = individualEntit;
    }

    public Task EnableOrDisableUser(Guid id, bool status)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByEmail(string email)
        => (await _userRepository.FindByCondition(x => x.Login.Email.ToLower() == email.ToLower())).FirstOrDefault();

    public Task InsertUser(Professional entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> ListUser()
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(Professional entity)
    {
        throw new NotImplementedException();
    }
}
