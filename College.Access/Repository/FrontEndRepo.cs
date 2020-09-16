using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.AcademicItemsDto;
using College.Model.DataTransferObject.AcademicsDto;
using College.Model.DataTransferObject.AttachmentDto;
using College.Model.DataTransferObject.CarouselDto;
using College.Model.DataTransferObject.PageDto;
using College.Model.DataTransferObject.PopupDto;
using College.Model.DataTransferObject.TeacherDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Access.Repository
{
    public class FrontEndRepo : IFrontEndRepo
    {
        private readonly CollegeContext _context;

        public FrontEndRepo(CollegeContext _context)
        {
            this._context = _context;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        #region Page

        /*
        public async Task<IList<PageModelDto>> FetchPageDataListAsyncTask()
        {
            return await (from pages in _context.Page
                          select new PageModelDto()
                          {
                              PageId = pages.PageId,
                              PageName = pages.PageName,
                              Description = pages.Description,
                              BackgroundImage = pages.BackgroundImage,
                          }).OrderBy(a => a.PageId).ToListAsync();
        }
        */
        public async Task<PageModelDto> FetchPageDataByIdAsyncTask(int id)
        {
            return await (from pages in _context.Page
                select new PageModelDto
                {
                    PageId = pages.PageId,
                    PageName = pages.PageName,
                    Description = WebUtility.HtmlDecode(pages.Description),
                    BackgroundImage = pages.BackgroundImage
                }).FirstOrDefaultAsync(a => a.PageId == id);
        }

        /*
        public async Task<bool> CreatePageDataAsyncTask(PageModelDto page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            var pages = new PageModel()
            {
                PageName = page.PageName,
                Description = page.Description,
                BackgroundImage = page.BackgroundImage,
                CreatedDate = DateTime.Now
            };
            await _context.Page.AddAsync(pages);
            return await SaveChangesAsyncTask();
        }
        */
        public async Task<bool> UpdatePageDataAsyncTask(PageModelDto page)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));
            var pages = new PageModel
            {
                PageId = page.PageId,
                PageName = page.PageName,
                Description = WebUtility.HtmlEncode(page.Description),
                BackgroundImage = page.BackgroundImage,
                DateUpdated = DateTime.Now
            };
            _context.Page.Update(pages);
            return await SaveChangesAsyncTask();
        }

        /*
        public async Task<bool> DeletePageDataAsyncTask(int id)
        {
            var page = await _context.Page.FirstOrDefaultAsync(a => a.PageId == id);
            _context.Page.Remove(page);
            return await SaveChangesAsyncTask();
        }
        */

        #endregion

        #region Carousel Model

        public async Task<IList<CarouselModelDto>> FetchCarouselListAsyncTask()
        {
            return await (from carousel in _context.Carousel
                select new CarouselModelDto
                {
                    CarouselId = carousel.CarouselId,
                    Title = carousel.Title,
                    Summary = WebUtility.HtmlDecode(carousel.Summary),
                    Image = carousel.Image
                }).OrderBy(a => a.CarouselId).ToListAsync();
        }

        public async Task<CarouselModelDto> FetchCarouselByIdAsyncTask(int id)
        {
            return await (from carousel in _context.Carousel
                select new CarouselModelDto
                {
                    CarouselId = carousel.CarouselId,
                    Title = carousel.Title,
                    Summary = WebUtility.HtmlDecode(carousel.Summary),
                    Image = carousel.Image
                }).FirstOrDefaultAsync(a => a.CarouselId == id);
        }

        public async Task<bool> CreateCarouselAsyncTask(CarouselModelDto carousel)
        {
            if (carousel == null)
                throw new ArgumentNullException(nameof(carousel));
            var carousels = new CarouselModel
            {
                CarouselId = carousel.CarouselId,
                Title = carousel.Title,
                Summary = WebUtility.HtmlEncode(carousel.Summary),
                Image = carousel.Image,
                CreatedDate = DateTime.Now
            };
            await _context.Carousel.AddAsync(carousels);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateCarouselAsyncTask(CarouselModelDto carousel)
        {
            if (carousel == null)
                throw new ArgumentNullException(nameof(carousel));
            var carouselModel = new CarouselModel
            {
                CarouselId = carousel.CarouselId,
                Title = carousel.Title,
                Summary = WebUtility.HtmlDecode(carousel.Summary),
                Image = carousel.Image,
                DateUpdated = DateTime.Now
            };
            _context.Carousel.Update(carouselModel);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> DeleteCarouselAsyncTask(int id)
        {
            var carousel = await _context.Carousel.FirstOrDefaultAsync(a => a.CarouselId == id);
            _context.Carousel.Remove(carousel);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region Academics

        /*
        public async Task<IList<AcademicModelDto>> FetchAcademicDataListAsyncTask()
        {
            return await (from academic in _context.Academics
                          select new AcademicModelDto()
                          {
                              AcademicId = academic.AcademicId,
                              Description = academic.Description,
                          }).OrderBy(a => a.AcademicId).ToListAsync();
        }
        */
        public async Task<AcademicModelDto> FetchAcademicDataByIdAsyncTask(int id)
        {
            return await (from academic in _context.Academics
                select new AcademicModelDto
                {
                    AcademicId = academic.AcademicId,
                    Description = WebUtility.HtmlDecode(academic.Description)
                }).FirstOrDefaultAsync(a => a.AcademicId == id);
        }

        public async Task<bool> UpdateAcademicDataAsyncTask(AcademicModelDto academic)
        {
            if (academic == null)
                throw new ArgumentNullException(nameof(academic));
            var academicModel = new AcademicsModel
            {
                AcademicId = academic.AcademicId,
                Description = WebUtility.HtmlEncode(academic.Description),
                DateUpdated = DateTime.Now
            };
            _context.Academics.Update(academicModel);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region AcademicItems

        public async Task<IList<AcademicItemsModelDto>> FetchAcademicItemListAsyncTask()
        {
            return await (from academicItems in _context.AcademicItems
                select new AcademicItemsModelDto
                {
                    ItemId = academicItems.ItemId,
                    Title = academicItems.Title,
                    Image = academicItems.Image,
                    Description = WebUtility.HtmlDecode(academicItems.Description),
                    Link = academicItems.Link
                }).OrderBy(a => a.ItemId).ToListAsync();
        }

        public async Task<AcademicItemsModelDto> FetchAcademicItemByIdAsyncTask(int id)
        {
            return await (from academicItems in _context.AcademicItems
                select new AcademicItemsModelDto
                {
                    ItemId = academicItems.ItemId,
                    Title = academicItems.Title,
                    Image = academicItems.Image,
                    Description = WebUtility.HtmlDecode(academicItems.Description),
                    Link = academicItems.Link
                }).FirstOrDefaultAsync(a => a.ItemId == id);
        }

        /*
        public async Task<bool> CreateAcademicItemAsyncTask(AcademicItemsModelDto academic)
        {
            if (academic == null)
                throw new ArgumentNullException(nameof(academic));
            var academicModel = new AcademicItemsModel()
            {
                ItemId = academic.ItemId,
                Title = academic.Title,
                Image = academic.Image,
                Description = academic.Description,
                Link = academic.Link,
                CreatedDate = DateTime.Now
            };
            await _context.AcademicItems.AddAsync(academicModel);
            return await SaveChangesAsyncTask();
        }
        */

        public async Task<bool> UpdateAcademicItemAsyncTask(AcademicItemsModelDto academic)
        {
            if (academic == null)
                throw new ArgumentNullException(nameof(academic));
            var academicModel = new AcademicItemsModel
            {
                ItemId = academic.ItemId,
                Title = academic.Title,
                Image = academic.Image,
                Description = WebUtility.HtmlEncode(academic.Description),
                Link = academic.Link,
                DateUpdated = DateTime.Now
            };
            _context.AcademicItems.Update(academicModel);
            return await SaveChangesAsyncTask();
        }

        /*
        public async Task<bool> DeleteAcademicItemAsyncTask(int id)
        {
            var academic = await _context.AcademicItems.FirstOrDefaultAsync(a => a.ItemId == id);
            _context.AcademicItems.Remove(academic);
            return await SaveChangesAsyncTask();
        }
        */

        #endregion

        #region Teacher

        public async Task<IList<TeacherModelDto>> FetchTeacherListAsyncTask()
        {
            return await (from teacher in _context.Teachers
                select new TeacherModelDto
                {
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherName,
                    Image = teacher.Image,
                    Designation = teacher.Designation,
                    Facebook = teacher.Facebook,
                    Instagram = teacher.Instagram,
                    Twitter = teacher.Twitter,
                    GooglePlus = teacher.GooglePlus
                }).OrderBy(a => a.TeacherId).ToListAsync();
        }

        public async Task<TeacherModelDto> FetchTeacherByIdAsyncTask(int id)
        {
            return await (from teacher in _context.Teachers
                select new TeacherModelDto
                {
                    TeacherId = teacher.TeacherId,
                    TeacherName = teacher.TeacherName,
                    Image = teacher.Image,
                    Designation = teacher.Designation,
                    Facebook = teacher.Facebook,
                    Instagram = teacher.Instagram,
                    Twitter = teacher.Twitter,
                    GooglePlus = teacher.GooglePlus
                }).FirstOrDefaultAsync(a => a.TeacherId == id);
        }

        public async Task<bool> CreateTeacherAsyncTask(TeacherModelDto teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));
            var model = new TeachersModel
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                Image = teacher.Image,
                Designation = teacher.Designation,
                Facebook = teacher.Facebook,
                Instagram = teacher.Instagram,
                Twitter = teacher.Twitter,
                GooglePlus = teacher.GooglePlus,
                CreatedDate = DateTime.Now
            };
            await _context.Teachers.AddAsync(model);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateTeacherAsyncTask(TeacherModelDto teacher)
        {
            if (teacher == null)
                throw new ArgumentNullException(nameof(teacher));
            var model = new TeachersModel
            {
                TeacherId = teacher.TeacherId,
                TeacherName = teacher.TeacherName,
                Image = teacher.Image,
                Designation = teacher.Designation,
                Facebook = teacher.Facebook,
                Instagram = teacher.Instagram,
                Twitter = teacher.Twitter,
                GooglePlus = teacher.GooglePlus,
                DateUpdated = DateTime.Now
            };
            _context.Teachers.Update(model);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> DeleteTeacherAsyncTask(int id)
        {
            var model = await _context.Teachers.FirstOrDefaultAsync(a => a.TeacherId == id);
            _context.Teachers.Remove(model);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region Popup

        public async Task<PopupModelDto> FetchPopUpByIdAsyncTask(int id)
        {
            return await (from popup in _context.Popup
                select new PopupModelDto
                {
                    PopupId = popup.PopupId,
                    Name = popup.Name,
                    Link = popup.Link
                }).FirstOrDefaultAsync(a => a.PopupId == id);
        }

        public async Task<bool> UpdatePopUpAsyncTask(PopupModelDto popup)
        {
            if (popup == null)
                throw new ArgumentNullException(nameof(popup));
            var model = new PopupModel
            {
                PopupId = popup.PopupId,
                Name = popup.Name,
                Link = popup.Link,
                DateUpdated = DateTime.Now
            };
            _context.Popup.Update(model);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region Attachment

        public async Task<AttachmentModelDto> FetchAttachmentByIdAsyncTask(int id)
        {
            return await (from attachment in _context.Attachment
                select new AttachmentModelDto
                {
                    AttachmentId = attachment.AttachmentId,
                    PageId = attachment.PageId,
                    Link = attachment.Link,
                    FileName = attachment.FileName
                }).FirstOrDefaultAsync(a => a.PageId == id);
        }

        public async Task<bool> UpdateAttachmentAsyncTask(AttachmentModelDto attachment)
        {
            if (attachment == null)
                throw new ArgumentNullException(nameof(attachment));
            var attachmentModel = new AttachmentModel
            {
                AttachmentId = attachment.AttachmentId,
                PageId = attachment.PageId,
                Link = attachment.Link,
                FileName = attachment.FileName
            };
            _context.Attachment.Update(attachmentModel);
            return await SaveChangesAsyncTask();
        }

        #endregion
    }
}