using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class FooterConf : IEntityTypeConfiguration<FooterHeaderModel>
    {
        public void Configure(EntityTypeBuilder<FooterHeaderModel> builder)
        {
            builder.HasKey(a => a.FooterHeaderId);
            builder.HasIndex(a => a.FooterHeaderId)
                .IsUnique();
            builder.Property(a => a.FooterHeaderId)
                .IsRequired();

            builder.Property(a => a.Slogan)
                .IsRequired();
            builder.Property(a => a.ContactEmail)
                .IsRequired();
            builder.Property(a => a.ContactNumber)
                .IsRequired();
            builder.Property(a => a.ContactAddress)
                .IsRequired();

            builder.Property(a => a.FacebookLink)
                .IsRequired(false);
            builder.Property(a => a.TweeterLink)
                .IsRequired(false);
            builder.Property(a => a.InstaGramLink)
                .IsRequired(false);
            builder.Property(a => a.GooglePlusLink)
                .IsRequired(false);
            builder.Property(a => a.YoutubeLink)
                .IsRequired(false);

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new FooterHeaderModel
            {
                FooterHeaderId = 1,
                Slogan =
                    "If you are going to use a passage of Lorem Ipsum you need to be sure there isn't anything embarrassing hidden in the middle of text",
                ContactEmail = "bishnudhani@gmail.com",
                ContactNumber = "084-404100",
                ContactAddress = "Barbardiya Municipality ward no 6, Jaynagar"
            });
        }
    }
}