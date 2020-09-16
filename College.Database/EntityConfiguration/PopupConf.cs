using System;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class PopupConf : IEntityTypeConfiguration<PopupModel>
    {
        public void Configure(EntityTypeBuilder<PopupModel> builder)
        {
            builder.HasKey(a => a.PopupId);
            builder.HasIndex(a => a.PopupId)
                .IsUnique();
            builder.Property(a => a.PopupId)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new PopupModel
            {
                PopupId = 1,
                Name = "Popup",
                CreatedDate = DateTime.Now,
                Link = "\\User_Information\\Popup\\Popup\\PopUp.jpg"
            });
        }
    }
}