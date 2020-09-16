using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class AppSettingsConf : IEntityTypeConfiguration<AppSettingsModel>
    {
        public void Configure(EntityTypeBuilder<AppSettingsModel> builder)
        {
            builder.HasKey(a => a.SettingsId);
            builder.HasIndex(a => a.SettingsId)
                .IsUnique();
            builder.Property(a => a.SettingsId)
                .IsRequired();
        }
    }
}