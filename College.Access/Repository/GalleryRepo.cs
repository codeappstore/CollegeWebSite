using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.GalleryDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Access.Repository
{
    public class GalleryRepo : IGalleryRepo
    {
        private readonly CollegeContext _context;

        public GalleryRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<IList<GalleryModelDto>> FetchAllGalleryAsyncTask()
        {
            return await (from item in _context.Gallery
                select new GalleryModelDto
                {
                    GalleryId = item.GalleryId,
                    Photographer = item.Photographer,
                    Thumbnail = item.Thumbnail,
                    Title = item.Title,
                    FileString = null
                }).ToListAsync();
        }

        public async Task<bool> UpdateGalleryAsyncTask(GalleryModelDto gallery)
        {
            var galleryToUpdate = await _context.Gallery.FirstOrDefaultAsync(g => g.GalleryId == gallery.GalleryId);
            galleryToUpdate.GalleryId = gallery.GalleryId;
            galleryToUpdate.Title = gallery.Title;
            galleryToUpdate.Thumbnail = gallery.Thumbnail;
            galleryToUpdate.Photographer = gallery.Photographer;
            galleryToUpdate.UpdatedAt = DateTime.Now;
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> CreateGalleryAsyncTask(GalleryModelDto gallery)
        {
            var galleryToAdd = new GalleryModel
            {
                Title = gallery.Title,
                Thumbnail = gallery.Thumbnail,
                Photographer = gallery.Photographer,
                CreatedAt = DateTime.Now
            };
            await _context.Gallery.AddAsync(galleryToAdd);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<GalleryModelDto> FetchGalleryAsyncTask(int id)
        {
            return await (from item in _context.Gallery
                select new GalleryModelDto
                {
                    GalleryId = item.GalleryId,
                    Photographer = item.Photographer,
                    Thumbnail = item.Thumbnail,
                    Title = item.Title,
                    FileString = null
                }).FirstOrDefaultAsync(a => a.GalleryId == id);
        }

        public async Task<bool> DeleteGalleryAsyncTask(int id)
        {
            var galleryToDelete = await _context.Gallery.FirstOrDefaultAsync(g => g.GalleryId == id);
            _context.Gallery.Remove(galleryToDelete);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IList<ImageModelDto>> FetchAllImageAsyncTask()
        {
            return await (from item in _context.Images
                select new ImageModelDto
                {
                    GalleryId = item.GalleryId,
                    Extension = item.Extension,
                    FileString = null,
                    ImageId = item.ImageId,
                    ImageLink = item.ImageLink,
                    Size = item.Size
                }).ToListAsync();
        }

        public async Task<IList<ImageModelDto>> FetchAllImageByGalleryIdAsyncTask(int id)
        {
            return await (from item in _context.Images
                select new ImageModelDto
                {
                    GalleryId = item.GalleryId,
                    Extension = item.Extension,
                    FileString = null,
                    ImageId = item.ImageId,
                    ImageLink = item.ImageLink,
                    Size = item.Size
                }).Where(a => a.GalleryId == id).ToListAsync();
        }

        public async Task<bool> UpdateImageAsyncTask(ImageModelDto image)
        {
            var imageToUpdate = await _context.Images.FirstOrDefaultAsync(a => a.ImageId == image.ImageId);
            imageToUpdate.ImageId = image.ImageId;
            imageToUpdate.GalleryId = image.GalleryId;
            imageToUpdate.Extension = image.Extension;
            imageToUpdate.ImageLink = image.ImageLink;
            imageToUpdate.Size = image.Size;
            imageToUpdate.UpdatedAt = DateTime.Now;
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> CreateImageAsyncTask(ImageModelDto image)
        {
            var imageToAdd = new ImagesModel
            {
                GalleryId = image.GalleryId,
                Extension = image.Extension,
                ImageLink = image.ImageLink,
                Size = image.Size,
                CreatedAt = DateTime.Now
            };
            await _context.Images.AddAsync(imageToAdd);
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<ImageModelDto> FetchImageAsyncTask(int id)
        {
            return await (from item in _context.Images
                select new ImageModelDto
                {
                    GalleryId = item.GalleryId,
                    Extension = item.Extension,
                    FileString = null,
                    ImageId = item.ImageId,
                    ImageLink = item.ImageLink,
                    Size = item.Size
                }).FirstOrDefaultAsync(a => a.ImageId == id);
        }

        public async Task<bool> DeleteImageAsyncTask(int id)
        {
            var imageToDelete = await _context.Images.FirstOrDefaultAsync(g => g.ImageId == id);
            _context.Images.Remove(imageToDelete);
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}