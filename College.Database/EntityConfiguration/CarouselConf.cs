using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class CarouselConf : IEntityTypeConfiguration<CarouselModel>
    {
        public void Configure(EntityTypeBuilder<CarouselModel> builder)
        {
            builder.HasKey(a => a.CarouselId);
            builder.HasIndex(a => a.CarouselId)
                .IsUnique();
            builder.Property(a => a.CarouselId)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");
        }
    }
}