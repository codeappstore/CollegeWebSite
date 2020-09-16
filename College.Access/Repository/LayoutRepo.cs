using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using College.Access.IRepository;
using College.Database.Context;
using College.Model.DataTransferObject.FooterDto;
using College.Model.DataTransferObject.ImportantLinksDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.DataTransferObject.SalientFeaturesDto;
using College.Model.DataTransferObject.StudentSayStudents;
using College.Model.DataTransferObject.StudentsSayDto;
using College.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace College.Access.Repository
{
    public class LayoutRepo : ILayoutRepo
    {
        private readonly CollegeContext _context;

        public LayoutRepo(CollegeContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsyncTask()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<FooterStudentSlogan> FetchFooterStudentSlogan()
        {
            var footerData = await _context.Footer.FirstOrDefaultAsync();
            var students = await _context.StudentSay.FirstOrDefaultAsync();
            var newDataSet = new FooterStudentSlogan
            {
                FooterSlogan = WebUtility.HtmlDecode(footerData.Slogan),
                StudentSaySlogan = students.Slogan
            };
            return newDataSet;
        }

        #region Footer Header

        public async Task<FooterUpdateModelDto> FetchFooterHeaderAsyncTask(int id)
        {
            return await (from footer in _context.Footer
                select new FooterUpdateModelDto
                {
                    FooterHeaderId = footer.FooterHeaderId,
                    Slogan = WebUtility.HtmlDecode(footer.Slogan),

                    ContactNumber = footer.ContactNumber,
                    ContactEmail = footer.ContactEmail,
                    ContactAddress = footer.ContactAddress,

                    FacebookLink = footer.FacebookLink,
                    TweeterLink = footer.TweeterLink,
                    InstaGramLink = footer.InstaGramLink,
                    GooglePlusLink = footer.GooglePlusLink,
                    YoutubeLink = footer.YoutubeLink
                }).FirstOrDefaultAsync(a => a.FooterHeaderId == id);
        }

        public async Task<bool> UpdateFooterHeaderAsyncTask(FooterUpdateModelDto footerUpdate)
        {
            if (footerUpdate == null)
                throw new ArgumentNullException(nameof(footerUpdate));
            var footerModel = new FooterHeaderModel
            {
                FooterHeaderId = footerUpdate.FooterHeaderId,
                Slogan = WebUtility.HtmlEncode(footerUpdate.Slogan),

                ContactNumber = footerUpdate.ContactNumber,
                ContactEmail = footerUpdate.ContactEmail,
                ContactAddress = footerUpdate.ContactAddress,

                FacebookLink = footerUpdate.FacebookLink,
                TweeterLink = footerUpdate.TweeterLink,
                InstaGramLink = footerUpdate.InstaGramLink,
                GooglePlusLink = footerUpdate.GooglePlusLink,
                YoutubeLink = footerUpdate.YoutubeLink,

                DateUpdated = DateTime.Now
            };

            _context.Footer.Update(footerModel);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region Salient Features

        public async Task<IList<SalientFeaturesModelDto>> FetchSalientFeaturesListAsyncTask()
        {
            return await (from feature in _context.SalientFeatures
                select new SalientFeaturesModelDto
                {
                    SalientFeatureId = feature.SalientFeatureId,
                    Feature = feature.Feature
                }).OrderBy(a => a.SalientFeatureId).ToListAsync();
        }

        public async Task<SalientFeaturesModelDto> FetchSalientFeaturesByIdAsyncTask(int id)
        {
            return await (from feature in _context.SalientFeatures
                select new SalientFeaturesModelDto
                {
                    SalientFeatureId = feature.SalientFeatureId,
                    Feature = feature.Feature
                }).FirstOrDefaultAsync(a => a.SalientFeatureId == id);
        }

        public async Task<bool> CreateSalientFeaturesAsyncTask(SalientFeaturesModelDto features)
        {
            if (features == null)
                throw new ArgumentNullException(nameof(features));
            var featureModel = new SalientFeatures
            {
                Feature = features.Feature,
                CreatedDate = DateTime.Now
            };
            await _context.SalientFeatures.AddAsync(featureModel);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateSalientFeaturesAsyncTask(SalientFeaturesModelDto features)
        {
            if (features == null)
                throw new ArgumentNullException(nameof(features));
            var featureModel = new SalientFeatures
            {
                SalientFeatureId = features.SalientFeatureId,
                Feature = features.Feature,
                DateUpdated = DateTime.Now
            };
            _context.SalientFeatures.Update(featureModel);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> DeleteSalientFeaturesAsyncTask(int id)
        {
            var feature = await _context.SalientFeatures.FirstOrDefaultAsync(a => a.SalientFeatureId == id);
            _context.SalientFeatures.Remove(feature);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region ImportantLinks

        public async Task<IList<ImportantLinksModelDto>> FetchImportantLinksListAsyncTask()
        {
            return await (from links in _context.ImportantLinks
                select new ImportantLinksModelDto
                {
                    ImportantLinkId = links.ImportantLinkId,
                    Title = links.Title,
                    Link = links.Link
                }).OrderBy(a => a.ImportantLinkId).ToListAsync();
        }

        public async Task<ImportantLinksModelDto> FetchImportantLinksByIdAsyncTask(int id)
        {
            return await (from links in _context.ImportantLinks
                select new ImportantLinksModelDto
                {
                    ImportantLinkId = links.ImportantLinkId,
                    Title = links.Title,
                    Link = links.Link
                }).FirstOrDefaultAsync(a => a.ImportantLinkId == id);
        }

        public async Task<bool> CreateImportantLinkAsyncTask(ImportantLinksModelDto links)
        {
            if (links == null)
                throw new ArgumentNullException(nameof(links));
            var linkModel = new ImportantLinks
            {
                Title = links.Title,
                Link = links.Link,
                CreatedDate = DateTime.Now
            };
            await _context.ImportantLinks.AddAsync(linkModel);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateImportantLinkAsyncTask(ImportantLinksModelDto links)
        {
            if (links == null)
                throw new ArgumentNullException(nameof(links));
            var linkModel = new ImportantLinks
            {
                ImportantLinkId = links.ImportantLinkId,
                Title = links.Title,
                Link = links.Link,
                DateUpdated = DateTime.Now
            };
            _context.ImportantLinks.Update(linkModel);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> DeleteImportantLinkAsyncTask(int id)
        {
            var links = await _context.ImportantLinks.FirstOrDefaultAsync(a => a.ImportantLinkId == id);
            _context.ImportantLinks.Remove(links);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region Student Say Slogans

        public async Task<StudentSayModelDto> FetchStudentsSayAsyncTask(int id)
        {
            return await (from say in _context.StudentSay
                select new StudentSayModelDto
                {
                    StudentSayId = say.StudentSayId,
                    Slogan = WebUtility.HtmlDecode(say.Slogan),
                    Image = say.BackgroundImage,
                    BackgroundImage = null
                }).FirstOrDefaultAsync(a => a.StudentSayId == id);
        }

        public async Task<bool> UpdateStudentSayAsyncTask(StudentSayModelDto studentSay)
        {
            if (studentSay == null)
                throw new ArgumentNullException(nameof(studentSay));
            var sayModel = new StudentSay
            {
                StudentSayId = studentSay.StudentSayId,
                Slogan = WebUtility.HtmlEncode(studentSay.Slogan),
                BackgroundImage = studentSay.Image,
                DateUpdated = DateTime.Now
            };

            _context.StudentSay.Update(sayModel);
            return await SaveChangesAsyncTask();
        }

        #endregion

        #region Students Say

        public async Task<IList<StudentSayStudentsModelDto>> FetchStudentsSayingListAsyncTask()
        {
            return await (from students in _context.StudentsSayStudents
                select new StudentSayStudentsModelDto
                {
                    StudentSayId = students.StudentSayId,
                    StudentName = students.StudentName,
                    StudentDesignation = students.StudentDesignation,
                    Image = students.Image,
                    ImageString = null,
                    Description = WebUtility.HtmlDecode(students.Description)
                }).OrderBy(a => a.StudentSayId).ToListAsync();
        }

        public async Task<StudentSayStudentsModelDto> FetchStudentsSayingByIdAsyncTask(int id)
        {
            return await (from students in _context.StudentsSayStudents
                select new StudentSayStudentsModelDto
                {
                    StudentSayId = students.StudentSayId,
                    StudentName = students.StudentName,
                    StudentDesignation = students.StudentDesignation,
                    Image = students.Image,
                    ImageString = null,
                    Description = WebUtility.HtmlDecode(students.Description)
                }).FirstOrDefaultAsync(a => a.StudentSayId == id);
        }

        public async Task<bool> CreateStudentsSayingAsyncTask(StudentSayStudentsModelDto sayStudents)
        {
            if (sayStudents == null)
                throw new ArgumentNullException(nameof(sayStudents));
            var student = new StudentsSayStudents
            {
                StudentName = sayStudents.StudentName,
                StudentDesignation = sayStudents.StudentDesignation,
                Image = sayStudents.Image,
                Description = WebUtility.HtmlEncode(sayStudents.Description),
                CreatedDate = DateTime.Now
            };
            await _context.StudentsSayStudents.AddAsync(student);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> UpdateStudentsSayingAsyncTask(StudentSayStudentsModelDto sayStudents)
        {
            if (sayStudents == null)
                throw new ArgumentNullException(nameof(sayStudents));
            var student = new StudentsSayStudents
            {
                StudentSayId = sayStudents.StudentSayId,
                StudentName = sayStudents.StudentName,
                StudentDesignation = sayStudents.StudentDesignation,
                Image = sayStudents.Image,
                Description = WebUtility.HtmlEncode(sayStudents.Description),
                DateUpdated = DateTime.Now
            };
            _context.StudentsSayStudents.Update(student);
            return await SaveChangesAsyncTask();
        }

        public async Task<bool> DeleteStudentsSayingAsyncTask(int id)
        {
            var students = await _context.StudentsSayStudents.FirstOrDefaultAsync(a => a.StudentSayId == id);
            _context.StudentsSayStudents.Remove(students);
            return await SaveChangesAsyncTask();
        }

        #endregion
    }
}