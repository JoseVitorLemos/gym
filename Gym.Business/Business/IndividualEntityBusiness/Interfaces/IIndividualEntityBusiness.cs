using Gym.Domain.Entities;

namespace Gym.Business.IndividualEntityBusiness;

public interface IIndividualEntityBusiness
{
    Task<List<IndividualEntity>> ListIndividualEntity();
    Task<IndividualEntity> GetIndividualEntity(Guid id);
    Task<IQueryable<IndividualEntity>> FindIndividualEntityByName(string name, int page, int pageSize);
    Task InsertIndividualEntity(IndividualEntity entity);
    Task UpdateIndividualEntity(IndividualEntity entity);
    Task EnableOrDisableIndividualEntity(Guid id, bool status);
}
