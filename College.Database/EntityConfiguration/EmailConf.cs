using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class EmailConf : IEntityTypeConfiguration<EmailModel>
    {
        public void Configure(EntityTypeBuilder<EmailModel> builder)
        {
            builder.HasKey(a => a.EmailId);
            builder.HasIndex(a => a.EmailId)
                .IsUnique();
            builder.Property(a => a.EmailId)
                .IsRequired();

            builder.Property(a => a.IsAvailable)
                .HasDefaultValue(true);

            builder.Property(a => a.IsMasterEmail)
                .HasDefaultValue(true);

            builder.Property(a => a.MailServer)
                .HasDefaultValue("smtp.gmail.com");

            builder.Property(a => a.SmtpPort)
                .HasDefaultValue(465);

            builder.Property(a => a.Email)
                .IsRequired();

            builder.Property(a => a.Password)
                .IsRequired();

            builder.Property(a => a.From)
                .IsRequired();

            builder.Property(a => a.From)
                .HasDefaultValue("Shahid Bishnu Dhani Memorial Polytechnic Institute");

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new EmailModel
            {
                EmailId = 1,
                Email = "bishnudhain2020@gmail.com",
                Password = "4k+S8mpm/2I+CwmEdUp+elHZK3wHudBBfiwPfu3gGPkK4v0FUbAUa8vQ+HLvkWx3"
            });
        }
    }
}