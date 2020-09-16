using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class GalleryConf : IEntityTypeConfiguration<GalleryModel>
    {
        public void Configure(EntityTypeBuilder<GalleryModel> builder)
        {
            builder.HasKey(a => a.GalleryId);
            builder.HasIndex(a => a.GalleryId)
                .IsUnique();
            builder.Property(a => a.GalleryId)
                .IsRequired();


            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GetDate()");
        }
    }
}