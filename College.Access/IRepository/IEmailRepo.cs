using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.EmailDto;
using College.Model.Models;

namespace College.Access.IRepository
{
    public interface IEmailRepo
    {
        // Saves the current context 
        Task<bool> SaveChangesAsyncTask();

        // Add Email
        Task<bool> AddNewEmailAsyncTask(EmailCreateModelDto emailCreateModel);

        // Update Email
        Task<bool> UpdateExistingEmailAsyncTask(EmailUpdateModelDto emailUpdateModel);

        // List Email
        Task<IList<EmailModel>> FetchEmailListAsyncTask();

        // Email By Id or email
        Task<EmailUpdateModelDto> FetchEmailByFilter(int? id = null, string email = null);

        // Delete Email
        Task<bool> DeleteEmailAsyncTask(int id);
    }
}