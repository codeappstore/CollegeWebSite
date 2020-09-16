using System;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace College.Database.EntityConfiguration
{
    public class AcademicItemsConf : IEntityTypeConfiguration<AcademicItemsModel>
    {
        public void Configure(EntityTypeBuilder<AcademicItemsModel> builder)
        {
            builder.HasKey(a => a.ItemId);
            builder.HasIndex(a => a.ItemId)
                .IsUnique();
            builder.Property(a => a.ItemId)
                .IsRequired();


            builder.Property(a => a.CreatedDate)
                .HasDefaultValueSql("GetDate()");

            builder.Property(a => a.DateUpdated)
                .HasDefaultValueSql("GetDate()");

            builder.HasData(new AcademicItemsModel
            {
                ItemId = 1,
                Title = "Forestry",
                Link = "/Home/Forestry",
                Description =
                    "&lt;p&gt;&lt;span style=&quot;color: #343a40; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;Forests are the most significant part of natural resources. Specially, the forests are our greatest property as the Nepali saying goes, &quot;Hariyo ban, Nepal ko dhan&quot;.&lt;/span&gt;&lt;/p&gt;",
                CreatedDate = DateTime.Now,
                Image = "\\User_Information\\Pages\\Academic Item\\Forestry\\course_img2.jpg"
            });

            builder.HasData(new AcademicItemsModel
            {
                ItemId = 2,
                Title = "Agriculture",
                Link = "/Home/Agriculture",
                Description =
                    "&lt;p&gt;&lt;span style=&quot;color: #343a40; font-family: Roboto, sans-serif; font-size: 16px; background-color: #ffffff;&quot;&gt;A degree in agriculture gives the students the knowledge and skills needed to manage agricultural and farm businesses, or to work in areas such as agricultural sales, food production and farming.&lt;/span&gt;&lt;/p&gt;",
                CreatedDate = DateTime.Now,
                Image = "\\User_Information\\Pages\\Academic Item\\Agriculture\\course_img.jpg"
            });
        }
    }
}