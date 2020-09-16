using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.RoleDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Access.Repository
{
    public class RoleRepo : IRoleRepo
    {
        private readonly CollegeContext _context;

        public RoleRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> CreateNewRoleAsyncTask(RoleCreateModelDto roleCreateModel)
        {
            if (roleCreateModel == null)
                throw new ArgumentNullException(nameof(roleCreateModel));
            await _context.Roles.AddAsync(new RoleModel
            {
                RoleName = roleCreateModel.RoleName,
                State = roleCreateModel.State,
                Description = roleCreateModel.Description,
                CreatedDate = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateExistingRoleAsyncTask(RoleUpdateModelDto roleUpdateModel)
        {
            if (roleUpdateModel == null)
                throw new ArgumentNullException(nameof(roleUpdateModel));
            _context.Roles.Update(new RoleModel
            {
                RoleId = roleUpdateModel.RoleId,
                RoleName = roleUpdateModel.RoleName,
                State = roleUpdateModel.State,
                Description = roleUpdateModel.Description,
                DateUpdated = DateTime.Now
            });
            return await SaveChangesAsyncTask();
        }

        public async Task<IList<RoleModel>> FetchRoleListAsyncTask()
        {
            return await _context.Roles.OrderByDescending(a => a.CreatedDate).ToListAsync();
        }

        public async Task<RoleModel> FetchRoleByFilter(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(a => a.RoleId == id);
        }

        public async Task<bool> DeleteRoleAsyncTask(int id)
        {
            var roleModel = await _context.Roles.FirstOrDefaultAsync(a => a.RoleId == id);
            _context.Roles.Remove(roleModel);
            return await SaveChangesAsyncTask();
        }
    }
}