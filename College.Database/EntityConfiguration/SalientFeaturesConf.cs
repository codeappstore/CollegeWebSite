using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class SalientFeaturesConf : IEntityTypeConfiguration<SalientFeatures>
    {
        public void Configure(EntityTypeBuilder<SalientFeatures> builder)
        {
            builder.HasKey(a => a.SalientFeatureId);
            builder.HasIndex(a => a.SalientFeatureId)
                .IsUnique();
            builder.Property(a => a.SalientFeatureId)
                .IsRequired();

            builder.Property(a => a.Feature)
                .IsRequired();

            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 1,
                Feature = "Situated in the peaceful area away from the noise of the city."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 2,
                Feature = "Well ventilated and echo free classrooms with comfortable seating arrangement."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 3,
                Feature = "E-library and 24/7 internet facilities."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 4,
                Feature = "Highly motivated teachers and professional faculties."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 5,
                Feature = "Well-equipped multidisciplinary laboratory."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 6,
                Feature = "Availability of enough land for practical activities."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 7,
                Feature =
                    "Good coordination with different governmental and non-governmental organizations and agencies."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 8,
                Feature =
                    "Inter-college student exchange programme with different forestry and agricultural colleges of Nepal."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 9,
                Feature = "Publication of periodic general and research journals."
            });
            builder.HasData(new SalientFeatures
            {
                SalientFeatureId = 10,
                Feature =
                    "Provision of project works, work experience programmes, etc. for integrating the theoretical course with real life experience."
            });
        }
    }
}