using CollegeWebsite.DataAccess.Models.Pages.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeWebsite.DataAccess.Models.Pages.EntityConfiguration
{
    public class StaticPagesConfig : IEntityTypeConfiguration<StaticPages>
    {
        public void Configure(EntityTypeBuilder<StaticPages> builder)
        {
            builder.HasKey(s => s.PageId);
            builder.HasIndex(s => s.PageId)
               .IsUnique();
            builder.Property(s => s.PageId)
                .IsRequired();

            builder.Property(s => s.PageTitle)
              .IsRequired();

            builder.Property(s => s.Action)
              .IsRequired();

            builder.Property(s => s.Controller)
              .IsRequired();
        }
    }
}
