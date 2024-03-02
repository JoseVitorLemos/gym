using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.EntitiesConfiguration;

public class ExerciseDbContext : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("EXERCISES");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NumberSeries).HasPrecision(2, 0).IsRequired();
        builder.Property(x => x.Repetitions).HasPrecision(2, 0).IsRequired();
        builder.Property(x => x.RestTime).HasPrecision(2, 0).IsRequired();

        builder.HasOne(x => x.Workout)
               .WithMany(x => x.Exercises)
               .HasForeignKey(fx => fx.WorkoutId);

        builder.HasOne(x => x.ImageExercise)
               .WithMany(x => x.Exercises)
               .HasForeignKey(fx => fx.ImageExerciseId);
    }
}
