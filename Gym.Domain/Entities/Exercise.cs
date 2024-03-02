using Gym.Helpers.Exceptions;
using Gym.Helpers.Utils;

namespace Gym.Domain.Entities;

public sealed class Exercise : BaseEntity
{
    public int NumberSeries { get; private set; }
    public int Repetitions { get; private set; }
    public int? RestTime { get; private set; }
    public Guid ImageExerciseId { get; private set; }
    public ImageExercise ImageExercise { get; set; }
    public Guid WorkoutId { get; private set; }
    public Workout Workout { get; set; }

    public Exercise(int numberSeries, int repetitions, Guid imageExerciseId, Guid workoutId, int? restTime = null)
    {
        Validations(numberSeries, repetitions, imageExerciseId, workoutId, restTime);

        NumberSeries = numberSeries;
        Repetitions = repetitions;
        RestTime = restTime;
        ImageExerciseId = imageExerciseId;
        WorkoutId = workoutId;
    }

    private void Validations(int numberOfSeries, int repetitions, Guid imageExerciseId, Guid workoutId, int? restTime = null)
    {
        GlobalException.When(numberOfSeries < 1, "Number of series is required.");
        GlobalException.When(repetitions < 1, "Repetitions of series is required.");
        GlobalException.When(!GuidValidations.IsValid(imageExerciseId), "imageExerciseId is required.");
        GlobalException.When(!GuidValidations.IsValid(workoutId), "WorkoutId is required.");
        GlobalException.When(Convert.ToInt32(restTime) < 1, "RestTime is required.");
    }
}
