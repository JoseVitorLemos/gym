using Clean.Arch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Arch.Data.EntitiesConfiguration;

public class WorkoutDbContext : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.ToTable("WORKOUTS");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.IndividualEntity)
               .WithMany(x => x.Workout)
               .HasForeignKey(fx => fx.IndividualEntityId);

        builder.HasOne(x => x.Personal)
               .WithMany(x => x.Workout)
               .OnDelete(DeleteBehavior.NoAction)
               .HasForeignKey(fx => fx.PersonalId);
    }
}
