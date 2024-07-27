using AutoMapper;
using Gym.Business.ExercisesBusiness;
using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.ExercisesService;

public class ExercisesService : IExercisesService
{
    private readonly IExercisesBusiness _ExercisesBusiness;
    private readonly IMapper _mapper;

    public ExercisesService(IExercisesBusiness ExercisesBusiness, IMapper mapper)
    {
        _ExercisesBusiness = ExercisesBusiness;
        _mapper = mapper;
    }

    public async Task<List<ExerciseDTO>> GetAll()
        => _mapper.Map<List<ExerciseDTO>>(await _ExercisesBusiness.GetAll());

    public async Task<ExerciseDTO> Get(Guid id)
        => _mapper.Map<ExerciseDTO>(await _ExercisesBusiness.Get(id));

    public async Task<bool> Post(ExerciseDTO entity)
        => await _ExercisesBusiness.Post(_mapper.Map<Exercise>(entity));

    public async Task<bool> Update(ExerciseDTO entity)
        => await _ExercisesBusiness.Update(_mapper.Map<Exercise>(entity));

    public async Task<bool> EnableOrDisable(Guid id, bool status)
        => await _ExercisesBusiness.EnableOrDisable(id, status);
}
