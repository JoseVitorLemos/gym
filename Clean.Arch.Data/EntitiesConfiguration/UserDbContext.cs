using Clean.Arch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Arch.Data.EntitiesConfiguration;

public class UserDbContext : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("USERS");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.IndividualEntity)
               .WithMany(x => x.User)
               .HasForeignKey(fx => fx.IndividualEntityId);

        builder.HasOne(x => x.Login)
               .WithMany(x => x.User)
               .HasForeignKey(fx => fx.LoginId);
    }
}
