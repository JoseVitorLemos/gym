using Gym.Services.DTO;

namespace Gym.Services.ProfessionalService;

public interface IProfessionalService
{
    Task<List<ProfessionalDTO>> ListProfessional();
    Task<ProfessionalDTO> GetProfessional(Guid id);
    Task<bool> InsertProfessional(ProfessionalDTO model);
    Task<bool> UpdateProfessional(ProfessionalDTO model);
    Task<bool> EnableOrDisableProfessional(Guid id, bool status);
}
