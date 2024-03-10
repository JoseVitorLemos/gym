using Gym.Domain.Entities;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Business.IndividualEntityBusiness;

public class IndividualEntityBusiness : IIndividualEntityBusiness
{
    private readonly IRepository<IndividualEntity> _individualEntityRepository;
    private readonly IUnitOfWork _unit;

    public IndividualEntityBusiness(IRepository<IndividualEntity> individualEntityRepository, IUnitOfWork unit)
    {
        _individualEntityRepository = individualEntityRepository;
        _unit = unit;
    }

    public async Task<IndividualEntity> GetIndividualEntity(Guid id)
        => await _individualEntityRepository.GetById(id);

    public async Task InsertIndividualEntity(IndividualEntity entity)
    {
        var cpfExists = await _individualEntityRepository
            .FindByCondition(x => x.Cpf == entity.Cpf);

        if (cpfExists.Any())
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Cpf already registered");

        await _individualEntityRepository.Insert(entity);
    }

    public async Task<IQueryable<IndividualEntity>> FindIndividualEntityByName(string name, int page, int pageSize)
        => await _individualEntityRepository.FindByCondition(x => x.Name.Contains(name), true, page, pageSize);

    public async Task<List<IndividualEntity>> ListIndividualEntity()
        => await _individualEntityRepository.GetAll();

    public async Task UpdateIndividualEntity(IndividualEntity entity)
        => await _individualEntityRepository.Update(entity);

    public async Task EnableOrDisableIndividualEntity(Guid id, bool status)
        => await _individualEntityRepository.EnableOrDisable(id, status);
}
