using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class PageListConf : IEntityTypeConfiguration<PageListModel>
    {
        public void Configure(EntityTypeBuilder<PageListModel> builder)
        {
            builder.HasKey(p => p.PageListId);
            builder.HasIndex(p => p.PageListId)
                .IsUnique();
            builder.Property(p => p.PageListId)
                .IsRequired();

            builder.Property(p => p.PageName)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new PageListModel
            {
                PageListId = 1,
                PageName = "Dashboard"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 2,
                PageName = "Users"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 3,
                PageName = "Roles"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 4,
                PageName = "Privilege"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 5,
                PageName = "Web Site"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 6,
                PageName = "Send Email"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 7,
                PageName = "Email Settings"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 8,
                PageName = "Account"
            });

            builder.HasData(new PageListModel
            {
                PageListId = 9,
                PageName = "Setting"
            });
        }
    }
}