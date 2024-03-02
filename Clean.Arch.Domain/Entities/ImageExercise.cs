using Clean.Arch.Helpers.Exceptions;

namespace Clean.Arch.Domain.Entities;

public sealed class ImageExercise : BaseEntity
{
    public string FileName { get; private set; }
    public string ExerciseName { get; private set; }
    public byte[] FileByte { get; private set; }
    public string FileType { get; private set; }

    public ICollection<Workout> Workout { get; set; }
    public ICollection<Exercise> Exercises { get; set; }

    public ImageExercise(string fileName, string exerciseName, byte[] fileByte, string fileType)
    {
        Validations(fileName, exerciseName, fileByte, fileType);

        FileName = fileName;
        ExerciseName = exerciseName;
        FileByte = fileByte;
        FileType = fileType;
    }

    private void Validations(string fileName, string exerciseName, byte[] fileByte, string fileType)
    {
        GlobalException.When(string.IsNullOrEmpty(fileName), "FileName is required.");
        GlobalException.When(string.IsNullOrEmpty(exerciseName), "ExerciseName is required.");
        GlobalException.When(fileByte.Length == 0, "FileByte is required.");
        GlobalException.When(string.IsNullOrEmpty(fileType), "FileByte is required.");
    }
}
