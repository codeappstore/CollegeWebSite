using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class AccessConf : IEntityTypeConfiguration<AccessModel>
    {
        public void Configure(EntityTypeBuilder<AccessModel> builder)
        {
            builder.HasKey(a => a.AccessId);
            builder.HasIndex(a => a.AccessId)
                .IsUnique();
            builder.Property(a => a.AccessId)
                .IsRequired();

            /* Foreign Key and 1-* relation with Role Table*/
            builder.HasOne<RoleModel>()
                .WithMany()
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            /* Foreign Key and 1-* relation with PageList Table*/
            builder.HasOne<PageListModel>()
                .WithMany()
                .HasForeignKey(p => p.PageListId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.Status)
                .HasDefaultValue(AccessStatus.Enabled);


            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new AccessModel
            {
                AccessId = 1,
                RoleId = 2,
                PageListId = 1
            });

            builder.HasData(new AccessModel
            {
                AccessId = 2,
                RoleId = 2,
                PageListId = 2
            });

            builder.HasData(new AccessModel
            {
                AccessId = 3,
                RoleId = 2,
                PageListId = 3
            });

            builder.HasData(new AccessModel
            {
                AccessId = 4,
                RoleId = 2,
                PageListId = 4
            });

            builder.HasData(new AccessModel
            {
                AccessId = 5,
                RoleId = 2,
                PageListId = 5
            });

            builder.HasData(new AccessModel
            {
                AccessId = 6,
                RoleId = 2,
                PageListId = 6
            });

            builder.HasData(new AccessModel
            {
                AccessId = 7,
                RoleId = 2,
                PageListId = 7
            });

            builder.HasData(new AccessModel
            {
                AccessId = 8,
                RoleId = 2,
                PageListId = 8
            });

            builder.HasData(new AccessModel
            {
                AccessId = 9,
                RoleId = 2,
                PageListId = 9
            });
        }
    }
}