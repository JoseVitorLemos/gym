using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.EntitiesConfiguration;

public class IndividualEntityDbContext : IEntityTypeConfiguration<IndividualEntity>
{
    public void Configure(EntityTypeBuilder<IndividualEntity> builder)
    {
        builder.ToTable("IndividualEntities");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.HasIndex(x => x.Cpf).IsUnique();
        builder.Property(x => x.Cpf).HasMaxLength(11).IsRequired();
        builder.Property(x => x.Gender).HasMaxLength(2).IsRequired();

        builder.HasOne(x => x.Login)
               .WithMany(x => x.IndividualEntity)
               .HasForeignKey(fx => fx.LoginId);
    }
}
