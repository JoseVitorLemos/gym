using Gym.Domain.Entities;
using Gym.Domain.Interfaces;

namespace Gym.Business.ImageExerciseBusiness;

public class ImageExerciseBusiness : IImageExerciseBusiness
{
    private readonly IRepository<ImageExercise> _workoutRepository;

    public ImageExerciseBusiness(IRepository<ImageExercise> workoutRepository)
        => _workoutRepository = workoutRepository;

    public async Task<List<ImageExercise>> ListImageExercise()
        => await _workoutRepository.GetAll();

    public async Task<ImageExercise> GetImageExercise(Guid id)
        => await _workoutRepository.GetById(id);

    public async Task InsertImageExercise(ImageExercise entity)
        => await _workoutRepository.Insert(entity);

    public async Task UpdateImageExercise(ImageExercise entity)
        => await _workoutRepository.Update(entity);

    public async Task EnableOrDisableImageExercise(Guid id, bool status)
        => await _workoutRepository.EnableOrDisable(id, status);
}
