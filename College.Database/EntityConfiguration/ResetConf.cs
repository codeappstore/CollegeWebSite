using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class ResetConf : IEntityTypeConfiguration<ResetModel>
    {
        public void Configure(EntityTypeBuilder<ResetModel> builder)
        {
            builder.HasKey(a => a.ResetId);
            builder.HasIndex(a => a.ResetId)
                .IsUnique();
            builder.Property(a => a.ResetId)
                .IsRequired();

            builder.Property(a => a.Email)
                .IsRequired();
            builder.Property(a => a.Token)
                .IsRequired();

            builder.Property(a => a.IssuedDateTime)
                .IsRequired();

            builder.Property(a => a.ExpirationDateTime)
                .IsRequired();
        }
    }
}