using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class ImportantLinksConf : IEntityTypeConfiguration<ImportantLinks>
    {
        public void Configure(EntityTypeBuilder<ImportantLinks> builder)
        {
            builder.HasKey(a => a.ImportantLinkId);
            builder.HasIndex(a => a.ImportantLinkId)
                .IsUnique();
            builder.Property(a => a.ImportantLinkId)
                .IsRequired();

            builder.Property(a => a.Title)
                .IsRequired();

            builder.Property(a => a.Link)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new ImportantLinks
            {
                ImportantLinkId = 1,
                Title = "Ctevt",
                Link = "http://ctevt.org.np/"
            });

            builder.HasData(new ImportantLinks
            {
                ImportantLinkId = 2,
                Title = "Barbardiyamun",
                Link = "http://barbardiyamun.gov.np/"
            });

            builder.HasData(new ImportantLinks
            {
                ImportantLinkId = 3,
                Title = "Daobardiya",
                Link = "https://daobardiya.moha.gov.np/"
            });
        }
    }
}