using AutoMapper;
using Gym.Business.IndividualEntityBusiness;
using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.IndividualEntityService;

public class IndividualEntityService : IIndividualEntityService
{
    private readonly IIndividualEntityBusiness _individualEntityBusiness;
    private readonly IMapper _mapper;

    public IndividualEntityService(IIndividualEntityBusiness individualEntityBusiness, IMapper mapper)
    {
        _individualEntityBusiness = individualEntityBusiness;
        _mapper = mapper;
    }

    public async Task<List<IndividualEntityDTO>> ListIndividualEntity()
        => _mapper.Map<List<IndividualEntityDTO>>(await _individualEntityBusiness.ListIndividualEntity());

    public async Task<IndividualEntityDTO> GetIndividualEntity(Guid id)
        => _mapper.Map<IndividualEntityDTO>(await _individualEntityBusiness.GetIndividualEntity(id));

    public async Task<List<IndividualEntityDTO>> FindIndividualEntityByName(string name, int page, int pageSize) 
        => _mapper.Map<List<IndividualEntityDTO>>(await _individualEntityBusiness.FindIndividualEntityByName(name, page, pageSize));

    public async Task InsertIndividualEntity(IndividualEntityDTO entity)
        => await _individualEntityBusiness.InsertIndividualEntity(_mapper.Map<IndividualEntity>(entity));

    public async Task UpdateIndividualEntity(IndividualEntityDTO entity)
        => await _individualEntityBusiness.UpdateIndividualEntity(_mapper.Map<IndividualEntity>(entity));

    public async Task EnableOrDisableIndividualEntity(Guid id, bool status)
        => await _individualEntityBusiness.EnableOrDisableIndividualEntity(id, status);
}
