using CollegeWebsite.DataAccess.Pages.EntityConfiguration;
using CollegeWebsite.DataAccess.Pages.Model;
using Microsoft.EntityFrameworkCore;

namespace CollegeWebsite.DataAccess
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
        {

        }
        public virtual DbSet<StaticPages> StaticPages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StaticPagesConfig());
        }
    }
}
