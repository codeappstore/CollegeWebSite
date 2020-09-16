using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class DownloadsConf : IEntityTypeConfiguration<DownloadsModel>
    {
        public void Configure(EntityTypeBuilder<DownloadsModel> builder)
        {
            builder.HasKey(a => a.FileId);
            builder.HasIndex(a => a.FileId)
                .IsUnique();
            builder.Property(a => a.FileId)
                .IsRequired();


            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GetDate()");
        }
    }
}