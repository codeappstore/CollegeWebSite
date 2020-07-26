using CollegeWebsite.DataAccess.Models.Pages.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollegeWebsite.DataAccess.Models.Pages.Services.IRepo
{
    public interface IStaticPagesRepo
    {
        Task<bool> SaveChangesAsyncTask();
        Task<string> GetNewIdAsyncTask();
        Task<IEnumerable<StaticPages>> GetAllStaticPagesAsyncTask();
        Task<StaticPages> CreateStaticPagesAsyncTask(StaticPages staticPages);
        Task<StaticPages> GetStaticPagesByIdAsyncTask(string staticPagesId);

        void UpdateStaticPages(StaticPages staticPages);
        void DeleteStaticPages(StaticPages staticPages);
    }
}
