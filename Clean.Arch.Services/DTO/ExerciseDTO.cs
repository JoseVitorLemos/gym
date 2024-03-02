using Clean.Arch.Validations;

namespace Clean.Arch.Services.DTO;

public class ExerciseDTO
{
    public Guid Id { get; set; }
    [GuidAnnotations(ErrorMessage = "PersonalId is required")]
    public Guid PersonalId { get; set; }
    [GuidAnnotations(ErrorMessage = "IndividualEntityId is required")]
    public Guid IndividualEntityId { get; set; }
    public int NumberSeries { get; set; }
    public int Repetitions { get; set; }
    public int? RestTime { get; set; }
    [GuidAnnotations(ErrorMessage = "ImageExerciseId is required")]
    public Guid ImageExerciseId { get; set; }
    [GuidAnnotations(ErrorMessage = "ExercisesId is required")]
    public Guid ExercisesId { get; set; }
}
