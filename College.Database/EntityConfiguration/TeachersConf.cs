using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class TeachersConf : IEntityTypeConfiguration<TeachersModel>
    {
        public void Configure(EntityTypeBuilder<TeachersModel> builder)
        {
            builder.HasKey(a => a.TeacherId);
            builder.HasIndex(a => a.TeacherId)
                .IsUnique();
            builder.Property(a => a.TeacherId)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");
        }
    }
}