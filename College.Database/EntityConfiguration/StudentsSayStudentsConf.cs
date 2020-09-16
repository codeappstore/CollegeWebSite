using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class StudentsSayStudentsConf : IEntityTypeConfiguration<StudentsSayStudents>
    {
        public void Configure(EntityTypeBuilder<StudentsSayStudents> builder)
        {
            builder.HasKey(a => a.StudentSayId);
            builder.HasIndex(a => a.StudentSayId)
                .IsUnique();
            builder.Property(a => a.StudentSayId)
                .IsRequired();

            builder.Property(a => a.StudentName)
                .IsRequired();

            builder.Property(a => a.StudentDesignation)
                .IsRequired();

            builder.Property(a => a.Image)
                .IsRequired();

            builder.Property(a => a.Description)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");
        }
    }
}