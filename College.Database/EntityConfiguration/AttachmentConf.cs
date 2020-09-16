using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class AttachmentConf : IEntityTypeConfiguration<AttachmentModel>
    {
        public void Configure(EntityTypeBuilder<AttachmentModel> builder)
        {
            builder.HasKey(a => a.AttachmentId);
            builder.HasIndex(a => a.AttachmentId)
                .IsUnique();
            builder.Property(a => a.AttachmentId)
                .IsRequired();

            /* Foreign Key and 1-* relation with role Table*/
            builder.HasOne<PageModel>()
                .WithMany()
                .HasForeignKey(r => r.PageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new AttachmentModel
            {
                AttachmentId = 1,
                PageId = 4,
                FileName = "Forestry Curriculum",
                Link = "\\User_Information\\Pages\\Forestry\\2019-09-29_Diploma-in-Forestry-Revised-2019.pdf"
            });
            builder.HasData(new AttachmentModel
            {
                AttachmentId = 2,
                PageId = 5,
                FileName = "Agriculture Curriculum"
            });
            builder.HasData(new AttachmentModel
            {
                AttachmentId = 3,
                PageId = 1,
                FileName = "Brochure",
                Link = "\\User_Information\\Pages\\Brochure\\5_6188035137427472707 (1).pdf"
            });

            builder.HasData(new AttachmentModel
            {
                AttachmentId = 4,
                PageId = 11,
                FileName = "Prospectus"
            });

            builder.HasData(new AttachmentModel
            {
                AttachmentId = 5,
                PageId = 12,
                FileName = "Notice"
            });
        }
    }
}