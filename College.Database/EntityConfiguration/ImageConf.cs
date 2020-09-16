using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class ImageConf : IEntityTypeConfiguration<ImagesModel>
    {
        public void Configure(EntityTypeBuilder<ImagesModel> builder)
        {
            builder.HasKey(a => a.ImageId);
            builder.HasIndex(a => a.ImageId)
                .IsUnique();
            builder.Property(a => a.ImageId)
                .IsRequired();

            /* Foreign Key and 1-* relation with Gallery Table*/
            builder.HasOne<GalleryModel>()
                .WithMany()
                .HasForeignKey(r => r.GalleryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GetDate()");
        }
    }
}