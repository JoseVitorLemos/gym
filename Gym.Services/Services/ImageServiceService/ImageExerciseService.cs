using AutoMapper;
using Gym.Business.ImageExerciseBusiness;
using Gym.Domain.Entities;
using Gym.Services.DTO;

namespace Gym.Services.ImageExerciseService;

public class ImageExerciseService : IImageExerciseService
{
    private readonly IImageExerciseBusiness _workoutBusiness;
    private readonly IMapper _mapper;

    public ImageExerciseService(IImageExerciseBusiness workoutBusiness, IMapper mapper)
    {
        _workoutBusiness = workoutBusiness;
        _mapper = mapper;
    }

    public async Task<List<ImageExerciseDTO>> ListImageExercise()
        => _mapper.Map<List<ImageExerciseDTO>>(await _workoutBusiness.ListImageExercise());

    public async Task<ImageExerciseDTO> GetImageExercise(Guid id)
        => _mapper.Map<ImageExerciseDTO>(await _workoutBusiness.GetImageExercise(id));

    public async Task InsertImageExercise(ImageExerciseDTO entity)
        => await _workoutBusiness.InsertImageExercise(_mapper.Map<ImageExercise>(entity));

    public async Task UpdateImageExercise(ImageExerciseDTO entity)
        => await _workoutBusiness.UpdateImageExercise(_mapper.Map<ImageExercise>(entity));

    public async Task EnableOrDisableImageExercise(Guid id, bool status)
        => await _workoutBusiness.EnableOrDisableImageExercise(id, status);
}
