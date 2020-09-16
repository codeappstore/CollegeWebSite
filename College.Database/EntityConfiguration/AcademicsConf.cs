using System;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class AcademicsConf : IEntityTypeConfiguration<AcademicsModel>
    {
        public void Configure(EntityTypeBuilder<AcademicsModel> builder)
        {
            builder.HasKey(a => a.AcademicId);
            builder.HasIndex(a => a.AcademicId)
                .IsUnique();
            builder.Property(a => a.AcademicId)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new AcademicsModel
            {
                AcademicId = 1,
                Description =
                    "<p><span style=\"color: #8d9297; font-family: Roboto, sans-serif; font-size: 16px; text-align: center; background-color: #ffffff;\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore.</span></p>",
                CreatedDate = DateTime.Now
            });
        }
    }
}