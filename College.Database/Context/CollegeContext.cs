using College.Database.EntityConfiguration;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Database.Context
{
    public class CollegeContext : DbContext
    {
        public CollegeContext(DbContextOptions<CollegeContext> options) : base(options)
        {
        }

        // Authorization and Users management
        public virtual DbSet<AuthModel> Authentications { get; set; }
        public virtual DbSet<PrivilegeModel> Privileges { get; set; }
        public virtual DbSet<RoleModel> Roles { get; set; }
        public virtual DbSet<ResetModel> Resets { get; set; }
        public virtual DbSet<EmailModel> Email { get; set; }

        // Page Management
        public virtual DbSet<FooterHeaderModel> Footer { get; set; }
        public virtual DbSet<ImportantLinks> ImportantLinks { get; set; }
        public virtual DbSet<SalientFeatures> SalientFeatures { get; set; }
        public virtual DbSet<StudentSay> StudentSay { get; set; }
        public virtual DbSet<StudentsSayStudents> StudentsSayStudents { get; set; }

        // Individual Pages
        public virtual DbSet<PageModel> Page { get; set; }
        public virtual DbSet<TeachersModel> Teachers { get; set; }
        public virtual DbSet<AttachmentModel> Attachment { get; set; }
        public virtual DbSet<AcademicItemsModel> AcademicItems { get; set; }
        public virtual DbSet<AcademicsModel> Academics { get; set; }
        public virtual DbSet<CarouselModel> Carousel { get; set; }
        public virtual DbSet<PopupModel> Popup { get; set; }
        public virtual DbSet<DownloadsModel> Downloads { get; set; }
        public virtual DbSet<GalleryModel> Gallery { get; set; }
        public virtual DbSet<ImagesModel> Images { get; set; }

        public virtual DbSet<AppSettingsModel> AppSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuthConf());
            builder.ApplyConfiguration(new PrivilegeConf());
            builder.ApplyConfiguration(new RoleConf());
            builder.ApplyConfiguration(new ResetConf());
            builder.ApplyConfiguration(new EmailConf());
            builder.ApplyConfiguration(new FooterConf());
            builder.ApplyConfiguration(new ImportantLinksConf());
            builder.ApplyConfiguration(new SalientFeaturesConf());
            builder.ApplyConfiguration(new StudentSayConf());
            builder.ApplyConfiguration(new StudentsSayStudentsConf());
            builder.ApplyConfiguration(new PageConf());
            builder.ApplyConfiguration(new TeachersConf());
            builder.ApplyConfiguration(new AttachmentConf());
            builder.ApplyConfiguration(new AcademicItemsConf());
            builder.ApplyConfiguration(new AcademicsConf());
            builder.ApplyConfiguration(new CarouselConf());
            builder.ApplyConfiguration(new PopupConf());
            builder.ApplyConfiguration(new DownloadsConf());
            builder.ApplyConfiguration(new GalleryConf());
            builder.ApplyConfiguration(new ImageConf());
            builder.ApplyConfiguration(new AppSettingsConf());
        }
    }
}