using Clean.Arch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Arch.Data.EntitiesConfiguration;

public class ProfessionalDbContext : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("PROFESSIONALS");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.IndividualEntity)
               .WithMany(x => x.Professional)
               .HasForeignKey(fx => fx.IndividualEntityId);
    }
}
