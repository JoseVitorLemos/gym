using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.EntitiesConfiguration;

public class LoginConfirmationDbContext : IEntityTypeConfiguration<LoginConfirmation>
{
    public void Configure(EntityTypeBuilder<LoginConfirmation> builder)
    {
        builder.ToTable("LoginConfirmations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code).HasMaxLength(6).IsRequired();

        builder.HasOne(x => x.Login)
               .WithMany(x => x.LoginConfirmation)
               .HasForeignKey(fx => fx.LoginId);
    }
}
