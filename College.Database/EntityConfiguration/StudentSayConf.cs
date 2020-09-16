using System;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class StudentSayConf : IEntityTypeConfiguration<StudentSay>
    {
        public void Configure(EntityTypeBuilder<StudentSay> builder)
        {
            builder.HasKey(a => a.StudentSayId);
            builder.HasIndex(a => a.StudentSayId)
                .IsUnique();
            builder.Property(a => a.StudentSayId)
                .IsRequired();

            builder.Property(a => a.Slogan)
                .IsRequired();

            builder.Property(a => a.Slogan)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new StudentSay
            {
                StudentSayId = 1,
                Slogan =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore.",
                BackgroundImage = "\\User_Information\\Pages\\Students Say\\Images\\teacher_bg.jpg",
                CreatedDate = DateTime.Now
            });
        }
    }
}