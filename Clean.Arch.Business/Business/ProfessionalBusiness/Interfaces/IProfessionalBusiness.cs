using Clean.Arch.Domain.Entities;

namespace Clean.Arch.Business.ProfessionalBusiness;

public interface IProfessionalBusiness
{
    Task<List<Professional>> ListProfessional();
    Task<Professional> GetProfessional(Guid id);
    Task<bool> InsertProfessional(Professional entity);
    Task<bool> UpdateProfessional(Professional entity);
    Task<bool> EnableOrDisableProfessional(Guid id, bool status);
}
