using Gym.Business.LoginBusiness;
using Gym.Domain.Entities;
using Gym.Domain.Enums;
using Gym.Domain.Interfaces;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;

namespace Gym.Business.IndividualEntityBusiness;

public class IndividualEntityBusiness : IIndividualEntityBusiness
{
    private readonly IRepository<IndividualEntity> _individualEntityRepository;
    private readonly ILoginBusiness _LoginBusiness;
    private readonly IUnitOfWork _unitOfWork;

    public IndividualEntityBusiness(IRepository<IndividualEntity> individualEntityRepository,
            IUnitOfWork unitOfWork, ILoginBusiness LoginBusiness)
    {
        _individualEntityRepository = individualEntityRepository;
        _unitOfWork = unitOfWork;
        _LoginBusiness = LoginBusiness;
    }

    public async Task<IndividualEntity> GetIndividualEntity(Guid id)
        => await _individualEntityRepository.GetById(id);

    public async Task InsertFitnessClient(IndividualEntity entity)
    {
        var cpfExists = await _individualEntityRepository
            .FindByCondition(x => x.Cpf == entity.Cpf);

        if (cpfExists.Any())
            throw new GlobalException(HttpStatusCodes.BadRequest,
                    "Cpf already registered");

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _LoginBusiness
            .UpdateRoleFromId(entity.LoginId, Roles.FitnessClient);

            await _individualEntityRepository.Insert(entity);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackAsync();
            throw new GlobalException(HttpStatusCodes.InternalServerError, e.Message);
        }
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
