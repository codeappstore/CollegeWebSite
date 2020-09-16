using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.RoleDto;
using College.Model.Models;

namespace College.Access.IRepository
{
    public interface IRoleRepo
    {
        // Saves the current context 
        Task<bool> SaveChangesAsyncTask();

        // Create Role
        Task<bool> CreateNewRoleAsyncTask(RoleCreateModelDto roleCreateModel);

        // Update Role
        Task<bool> UpdateExistingRoleAsyncTask(RoleUpdateModelDto roleUpdateModel);

        // List Role
        Task<IList<RoleModel>> FetchRoleListAsyncTask();

        // Role By Id 
        Task<RoleModel> FetchRoleByFilter(int id);

        // Delete Role
        Task<bool> DeleteRoleAsyncTask(int id);
    }
}