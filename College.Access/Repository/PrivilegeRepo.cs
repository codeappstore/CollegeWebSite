using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.PrivilegeDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Access.Repository
{
    public class PrivilegeRepo : IPrivilegeRepo
    {
        private readonly CollegeContext _context;

        public PrivilegeRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> CreateNewPrivilegeAsyncTask(PrivilegeCreateModelDto privilegeCreateModel)
        {
            if (privilegeCreateModel == null)
                throw new ArgumentNullException(nameof(privilegeCreateModel));
            await _context.Privileges.AddAsync(new PrivilegeModel
            {
                RoleId = privilegeCreateModel.RoleId,
                Read = privilegeCreateModel.Read,
                Create = privilegeCreateModel.Create,
                Update = privilegeCreateModel.Update,
                Delete = privilegeCreateModel.Delete,
                CreatedDate = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateExistingPrivilegeAsyncTask(PrivilegeUpdateModelDto privilegeUpdateModel)
        {
            if (privilegeUpdateModel == null)
                throw new ArgumentNullException(nameof(privilegeUpdateModel));
            _context.Privileges.Update(new PrivilegeModel
            {
                PrivilegeId = privilegeUpdateModel.PrivilegeId,
                RoleId = privilegeUpdateModel.RoleId,
                Read = privilegeUpdateModel.Read,
                Create = privilegeUpdateModel.Create,
                Update = privilegeUpdateModel.Update,
                Delete = privilegeUpdateModel.Delete,
                DateUpdated = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<IList<PrivilegeModel>> FetchPrivilegeListAsyncTask()
        {
            return await _context.Privileges.OrderByDescending(a => a.CreatedDate).ToListAsync();
        }

        public async Task<PrivilegeModel> FetchPrivilegeByFilter(int id)
        {
            return await _context.Privileges.FirstOrDefaultAsync(a => a.PrivilegeId == id);
        }

        public async Task<bool> DeletePrivilegeAsyncTask(int id)
        {
            var privilegeModel = await _context.Privileges.FirstOrDefaultAsync(a => a.PrivilegeId == id);
            _context.Privileges.Remove(privilegeModel);
            return await SaveChangesAsyncTask();
        }
    }
}