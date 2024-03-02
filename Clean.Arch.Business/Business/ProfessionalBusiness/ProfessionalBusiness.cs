using Clean.Arch.Domain.Entities;
using Clean.Arch.Domain.Interfaces;

namespace Clean.Arch.Business.ProfessionalBusiness;

public class ProfessionalBusiness : IProfessionalBusiness
{
    private readonly IRepository<Professional> _workoutRepository;

    public ProfessionalBusiness(IRepository<Professional> workoutRepository)
        => _workoutRepository = workoutRepository;

    public async Task<List<Professional>> ListProfessional()
        => await _workoutRepository.GetAll();

    public async Task<Professional> GetProfessional(Guid id)
        => await _workoutRepository.GetById(id);

    public async Task<bool> InsertProfessional(Professional entity)
        => await _workoutRepository.Insert(entity);

    public async Task<bool> UpdateProfessional(Professional entity)
        => await _workoutRepository.Update(entity);

    public async Task<bool> EnableOrDisableProfessional(Guid id, bool status)
        => await _workoutRepository.EnableOrDisable(id, status);
}
