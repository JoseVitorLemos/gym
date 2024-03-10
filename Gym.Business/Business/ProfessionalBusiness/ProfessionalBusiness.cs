using Gym.Domain.Entities;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Business.ProfessionalBusiness;

public class ProfessionalBusiness : IProfessionalBusiness
{
    private readonly IRepository<Professional> _profissionalRepository;
    private readonly IRepository<Login> _loginRepositoy;
    private readonly IRepository<IndividualEntity> _invividualEntityRepositoy;

    public ProfessionalBusiness(IRepository<Professional> workoutRepository,
        IRepository<Login> loginRepository,
        IRepository<IndividualEntity> invividualEntityRepositoy)
    {
        _profissionalRepository = workoutRepository;
        _loginRepositoy = loginRepository;
        _invividualEntityRepositoy = invividualEntityRepositoy;
    }

    public async Task<List<Professional>> ListProfessional()
        => await _profissionalRepository.GetAll();

    public async Task<Professional> GetProfessional(Guid id)
        => await _profissionalRepository.GetById(id);

    public async Task<bool> InsertProfessional(Professional entity)
    {
        Guid loginId = entity.IndividualEntity.LoginId;
        Login login = await _loginRepositoy.GetById(loginId);

        if (login is null)
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Invalid login provided");


        var professional = await _profissionalRepository
            .FindByCondition(x => x.IndividualEntity.LoginId.Equals(loginId));

        if (professional.Any())
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Professional already registered");

        return await _profissionalRepository.Insert(entity);
    }

    public async Task<bool> UpdateProfessional(Professional entity)
        => await _profissionalRepository.Update(entity);

    public async Task<bool> EnableOrDisableProfessional(Guid id, bool status)
        => await _profissionalRepository.EnableOrDisable(id, status);
}
