using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.PrivilegeDto;
using College.Model.Models;

namespace College.Access.IRepository
{
    public interface IPrivilegeRepo
    {
        // Saves the current context 
        Task<bool> SaveChangesAsyncTask();

        // Create Privilege
        Task<bool> CreateNewPrivilegeAsyncTask(PrivilegeCreateModelDto privilegeCreateModel);

        // Update Privilege
        Task<bool> UpdateExistingPrivilegeAsyncTask(PrivilegeUpdateModelDto privilegeUpdateModel);

        // List Privilege
        Task<IList<PrivilegeModel>> FetchPrivilegeListAsyncTask();

        // Privilege By Id 
        Task<PrivilegeModel> FetchPrivilegeByFilter(int id);

        // Delete Privilege
        Task<bool> DeletePrivilegeAsyncTask(int id);
    }
}