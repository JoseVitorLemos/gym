using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.EntitiesConfiguration;

public class ImageExerciseDbContext : IEntityTypeConfiguration<ImageExercise>
{
    public void Configure(EntityTypeBuilder<ImageExercise> builder)
    {
        builder.ToTable("IMAGE_EXERCISES");
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.ExerciseName).IsUnique();
        builder.Property(x => x.FileName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ExerciseName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.FileByte).IsRequired();
        builder.Property(x => x.FileType).HasMaxLength(6).IsRequired();
    }
}
