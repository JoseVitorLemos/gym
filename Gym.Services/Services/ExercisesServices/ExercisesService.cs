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

    public async Task<List<ExerciseDTO>> ListExercises()
        => _mapper.Map<List<ExerciseDTO>>(await _ExercisesBusiness.ListExercises());

    public async Task<ExerciseDTO> GetExercises(Guid id)
        => _mapper.Map<ExerciseDTO>(await _ExercisesBusiness.GetExercises(id));

    public async Task<bool> InsertExercises(ExerciseDTO entity)
        => await _ExercisesBusiness.InsertExercises(_mapper.Map<Exercise>(entity));

    public async Task<bool> UpdateExercises(ExerciseDTO entity)
        => await _ExercisesBusiness.UpdateExercises(_mapper.Map<Exercise>(entity));

    public async Task<bool> EnableOrDisableExercises(Guid id, bool status)
        => await _ExercisesBusiness.EnableOrDisableExercises(id, status);
}
