using AutoMapper;
using Gym.Business.ProfessionalBusiness;
using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.ProfessionalService;

public class ProfessionalService : IProfessionalService
{
    private readonly IProfessionalBusiness _professionalBusiness;
    private readonly IMapper _mapper;

    public ProfessionalService(IProfessionalBusiness professionalBusiness, IMapper mapper)
    {
        _professionalBusiness = professionalBusiness;
        _mapper = mapper;
    }

    public async Task<List<ProfessionalDTO>> ListProfessional()
        => _mapper.Map<List<ProfessionalDTO>>(await _professionalBusiness.ListProfessional());

    public async Task<ProfessionalDTO> GetProfessional(Guid id)
        => _mapper.Map<ProfessionalDTO>(await _professionalBusiness.GetProfessional(id));

    public async Task<bool> InsertProfessional(ProfessionalDTO entity)
        => await _professionalBusiness.InsertProfessional(_mapper.Map<Professional>(entity));

    public async Task<bool> UpdateProfessional(ProfessionalDTO entity)
        => await _professionalBusiness.UpdateProfessional(_mapper.Map<Professional>(entity));

    public async Task<bool> EnableOrDisableProfessional(Guid id, bool status)
        => await _professionalBusiness.EnableOrDisableProfessional(id, status);
}
