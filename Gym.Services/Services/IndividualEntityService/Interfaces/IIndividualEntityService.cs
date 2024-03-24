using Gym.Services.DTO;

namespace Gym.Services.IndividualEntityService;

public interface IIndividualEntityService
{
    Task<List<IndividualEntityDTO>> ListIndividualEntity();
    Task<IndividualEntityDTO> GetIndividualEntity(Guid id);
    Task<List<IndividualEntityDTO>> FindIndividualEntityByName(string name, int page, int pageSize);
    Task InsertFitnessClient(IndividualEntityDTO model);
    Task UpdateIndividualEntity(IndividualEntityDTO model);
    Task EnableOrDisableIndividualEntity(Guid id, bool status);
}
