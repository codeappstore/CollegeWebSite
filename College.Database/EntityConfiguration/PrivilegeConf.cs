using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class PrivilegeConf : IEntityTypeConfiguration<PrivilegeModel>
    {
        public void Configure(EntityTypeBuilder<PrivilegeModel> builder)
        {
            builder.HasKey(p => p.PrivilegeId);
            builder.HasIndex(p => p.PrivilegeId)
                .IsUnique();
            builder.Property(p => p.PrivilegeId)
                .IsRequired();

            /* Foreign Key and 1-* relation with Role Table*/
            builder.HasOne<RoleModel>()
                .WithMany()
                .HasForeignKey(r => r.PrivilegeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new PrivilegeModel
            {
                // Admin
                PrivilegeId = 1,
                RoleId = 1,
                Read = true,
                Create = true,
                Update = true,
                Delete = true
            });

            builder.HasData(new PrivilegeModel
            {
                // Developer
                PrivilegeId = 2,
                RoleId = 2,
                Read = true,
                Create = true,
                Update = true,
                Delete = true
            });

            builder.HasData(new PrivilegeModel
            {
                // Manager
                PrivilegeId = 3,
                RoleId = 3,
                Read = true,
                Create = false,
                Update = true,
                Delete = false
            });
        }
    }
}