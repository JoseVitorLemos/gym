using Clean.Arch.Domain.Entities;

namespace Clean.Arch.Business.ImageExerciseBusiness;

public interface IImageExerciseBusiness
{
    Task<List<ImageExercise>> ListImageExercise();
    Task<ImageExercise> GetImageExercise(Guid id);
    Task InsertImageExercise(ImageExercise entity);
    Task UpdateImageExercise(ImageExercise entity);
    Task EnableOrDisableImageExercise(Guid id, bool status);
}
