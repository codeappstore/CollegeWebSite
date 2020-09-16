using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.DownloadsDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Access.Repository
{
    public class DownloadsRepo : IDownloadsRepo
    {
        private readonly CollegeContext _context;

        public DownloadsRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<IList<DownloadModelDto>> FetchAllDownloadsAsyncTask()
        {
            return await (from items in _context.Downloads
                select new DownloadModelDto
                {
                    FileId = items.FileId,
                    Extension = items.Extension,
                    FileLink = items.FileLink,
                    Size = items.Size,
                    Title = items.Title
                }).ToListAsync();
        }

        public async Task<bool> UpdateDownloadAsyncTask(DownloadModelDto download)
        {
            var downloadToUpdate = await _context.Downloads.FirstOrDefaultAsync(a => a.FileId == download.FileId);
            downloadToUpdate.Title = download.Title;
            downloadToUpdate.UpdatedAt = DateTime.Now;
            downloadToUpdate.Extension = download.Extension;
            downloadToUpdate.FileLink = download.FileLink;
            downloadToUpdate.Size = download.Size;
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> CreateDownloadAsyncTask(DownloadModelDto download)
        {
            var downloadToCreate = new DownloadsModel
            {
                Title = download.Title,
                Extension = download.Extension,
                FileLink = download.FileLink,
                Size = download.Size,
                CreatedAt = DateTime.Now
            };
            await _context.Downloads.AddAsync(downloadToCreate);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<DownloadModelDto> FetchDownloadAsyncTask(int id)
        {
            return await (from items in _context.Downloads
                select new DownloadModelDto
                {
                    FileId = items.FileId,
                    Extension = items.Extension,
                    FileLink = items.FileLink,
                    Size = items.Size,
                    Title = items.Title
                }).FirstOrDefaultAsync(a => a.FileId == id);
        }

        public async Task<bool> DeleteDownloadAsyncTask(int id)
        {
            var downloadToDelete = await _context.Downloads.FirstOrDefaultAsync(a => a.FileId == id);
            _context.Downloads.Remove(downloadToDelete);
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}