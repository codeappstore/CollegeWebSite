using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace College.Database.EntityConfiguration
{
    public class AuthConf : IEntityTypeConfiguration<AuthModel>
    {
        public void Configure(EntityTypeBuilder<AuthModel> builder)
        {
            builder.HasKey(a => a.AuthId);
            builder.HasIndex(a => a.AuthId)
                .IsUnique();
            builder.Property(a => a.AuthId)
                .IsRequired();

            /* Foreign Key and 1-* relation with role Table*/
            builder.HasOne<RoleModel>()
                .WithMany()
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            // Unique
            builder.HasIndex(a => a.UserName)
                .IsUnique();
            builder.HasIndex(a => a.Email)
                .IsUnique();

            builder.HasIndex(a => a.PhoneNumber)
                .IsUnique();


            builder.Property(a => a.IsEmailVerified)
                .HasDefaultValue(0);
            builder.Property(a => a.DateEmailVerified)
                .HasDefaultValueSql("GetDate()");
            builder.Property(a => a.Allowed)
                .HasDefaultValue(IsAllowed.Enabled);


            builder.Property(a => a.Image)
                .IsRequired(false);
            builder.Property(a => a.PhoneNumber)
                .IsRequired(false);
            builder.Property(a => a.Address)
                .IsRequired(false);

            builder.Property(a => a.DateOfBirth)
                .IsRequired();
            builder.Property(a => a.Gender)
                .IsRequired();


            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new AuthModel
            {
                AuthId = 1,
                RoleId = 2,
                FullName = "Dibesh Raj Subedi",
                UserName = "Developer Dibesh",
                DateOfBirth = new DateTime(1999, 07, 14),
                Address = "Gaushala, Kathmendu",
                PhoneNumber = "9861315234",
                Gender = AuthGender.Male,
                Email = "kingraj530@gmail.com",
                Password =
                    "37c217bb02332590ff91d81a5bea827ed8f5c5db75491a786f453acb6b970ec8",
                IsEmailVerified = true,
                DateEmailVerified = DateTime.Now
            });


            builder.HasData(new AuthModel
            {
                AuthId = 2,
                RoleId = 1,
                FullName = "Shiva Poudel",
                UserName = "Shiva",
                DateOfBirth = new DateTime(1994, 07, 14),
                Address = "Baneshowr, Kathmendu",
                PhoneNumber = "9801061035",
                Gender = AuthGender.Male,
                Email = "leo.shivapoudel@gmail.com",
                Password = "f804fb935ebf257c3c4c94591aef12692e6413e687e3451e00be2132914e2687",
                IsEmailVerified = true,
                DateEmailVerified = DateTime.Now
            });
        }
    }
}