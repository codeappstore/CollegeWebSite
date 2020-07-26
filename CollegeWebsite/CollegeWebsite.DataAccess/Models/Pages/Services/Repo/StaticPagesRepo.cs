using CollegeWebsite.DataAccess.Helper;
using CollegeWebsite.DataAccess.Models.Pages.Model;
using CollegeWebsite.DataAccess.Models.Pages.Services.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeWebsite.DataAccess.Models.Pages.Services.Repo
{
    public class StaticPagesRepo : IStaticPagesRepo
    {
        private readonly CollegeDbContext _context;

        public StaticPagesRepo(CollegeDbContext _context)
        {
            this._context = _context;
        }
        public async Task<StaticPages> CreateStaticPagesAsyncTask(StaticPages staticPages)
        {
            if (staticPages == null)
            {
                throw new ArgumentNullException(nameof(staticPages));
            }

            staticPages.PageId = await GetNewIdAsyncTask();
            await _context.StaticPages.AddAsync(staticPages);

            return staticPages;
        }

        public void DeleteStaticPages(StaticPages staticPages)
        {
            if (staticPages == null)
            {
                throw new ArgumentNullException(nameof(staticPages));
            }

            _context.StaticPages.Remove(staticPages);
        }

        public async Task<IEnumerable<StaticPages>> GetAllStaticPagesAsyncTask()
        {
            return await _context.StaticPages.ToListAsync();
        }

        public async Task<string> GetNewIdAsyncTask()
        {
            RandomStringBuilder builder = new RandomStringBuilder();
            string NewId = builder.RandStringGenerator((int)RandomStringBuilder.PorposeOfString.ID, 10);
            bool idExists = (await _context.StaticPages.AnyAsync(d => d.PageId == NewId));
            if (idExists)
            {
                return await GetNewIdAsyncTask();
            }
            else
            {
                return NewId;
            };
        }

        public async Task<StaticPages> GetStaticPagesByIdAsyncTask(string staticPagesId)
        {
            if (staticPagesId == null)
            {
                throw new ArgumentNullException(nameof(staticPagesId));
            }
            return await _context.StaticPages.FirstOrDefaultAsync(c => c.PageId == staticPagesId);
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateStaticPages(StaticPages staticPages)
        {
            if (staticPages == null)
            {
                throw new ArgumentNullException(nameof(staticPages));
            }

            _context.StaticPages.Update(staticPages);
        }
    }
}
