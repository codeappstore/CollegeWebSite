using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class RoleConf : IEntityTypeConfiguration<RoleModel>
    {
        public void Configure(EntityTypeBuilder<RoleModel> builder)
        {
            builder.HasKey(r => r.RoleId);
            builder.HasIndex(r => r.RoleId)
                .IsUnique();
            builder.Property(r => r.RoleId)
                .IsRequired();

            builder.HasIndex(r => r.RoleName)
                .IsUnique();


            builder.Property(r => r.Description)
                .IsRequired(false);


            builder.Property(r => r.State)
                .HasDefaultValue(RoleStatus.Enabled);


            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new RoleModel
            {
                RoleId = 1,
                RoleName = "Administrator",
                State = RoleStatus.Enabled,
                Description = "Role for Administrator"
            });

            builder.HasData(new RoleModel
            {
                RoleId = 2,
                RoleName = "Developer",
                State = RoleStatus.Enabled,
                Description = "Role for Developer"
            });

            builder.HasData(new RoleModel
            {
                RoleId = 3,
                RoleName = "Manager",
                State = RoleStatus.Enabled,
                Description = "Role for Manager"
            });
        }
    }
}