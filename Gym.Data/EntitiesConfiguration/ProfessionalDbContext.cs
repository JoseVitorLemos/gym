using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.EntitiesConfiguration;

public class ProfessionalDbContext : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("PROFESSIONALS");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.IndividualEntityId).IsUnique();

        builder.HasOne(x => x.IndividualEntity)
               .WithMany(x => x.Professional)
               .HasForeignKey(fx => fx.IndividualEntityId);
    }
}
