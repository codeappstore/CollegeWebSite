using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.FooterDto;
using College.Model.DataTransferObject.ImportantLinksDto;
using College.Model.DataTransferObject.OtherDto;
using College.Model.DataTransferObject.SalientFeaturesDto;
using College.Model.DataTransferObject.StudentSayStudents;
using College.Model.DataTransferObject.StudentsSayDto;

namespace College.Access.IRepository
{
    public interface ILayoutRepo
    {
        //   Saves the current context 
        Task<bool> SaveChangesAsyncTask();

        // Slogans
        Task<FooterStudentSlogan> FetchFooterStudentSlogan();

        #region Footer Header

        // Fetch Footer Header
        Task<FooterUpdateModelDto> FetchFooterHeaderAsyncTask(int id);

        // Update Footer Header
        Task<bool> UpdateFooterHeaderAsyncTask(FooterUpdateModelDto footerUpdate);

        #endregion

        #region Features

        //  Features list
        Task<IList<SalientFeaturesModelDto>> FetchSalientFeaturesListAsyncTask();

        //  Features by id
        Task<SalientFeaturesModelDto> FetchSalientFeaturesByIdAsyncTask(int id);

        //  Features Create
        Task<bool> CreateSalientFeaturesAsyncTask(SalientFeaturesModelDto features);

        //  Features Update
        Task<bool> UpdateSalientFeaturesAsyncTask(SalientFeaturesModelDto features);

        //  Features Delete
        Task<bool> DeleteSalientFeaturesAsyncTask(int id);

        #endregion

        #region Important Link

        // Important Links list
        Task<IList<ImportantLinksModelDto>> FetchImportantLinksListAsyncTask();

        //  Links by id
        Task<ImportantLinksModelDto> FetchImportantLinksByIdAsyncTask(int id);

        //  Links Create
        Task<bool> CreateImportantLinkAsyncTask(ImportantLinksModelDto links);

        //  Links Update
        Task<bool> UpdateImportantLinkAsyncTask(ImportantLinksModelDto links);

        //  Links Delete
        Task<bool> DeleteImportantLinkAsyncTask(int id);

        #endregion

        #region Students say

        // Fetch Student Say
        Task<StudentSayModelDto> FetchStudentsSayAsyncTask(int id);

        // Update Student Say
        Task<bool> UpdateStudentSayAsyncTask(StudentSayModelDto studentSay);

        #endregion

        #region Students

        // Students list
        Task<IList<StudentSayStudentsModelDto>> FetchStudentsSayingListAsyncTask();

        //  Students by id
        Task<StudentSayStudentsModelDto> FetchStudentsSayingByIdAsyncTask(int id);

        //  Students Create
        Task<bool> CreateStudentsSayingAsyncTask(StudentSayStudentsModelDto sayStudents);

        //  Students Update
        Task<bool> UpdateStudentsSayingAsyncTask(StudentSayStudentsModelDto sayStudents);

        //  Students Delete
        Task<bool> DeleteStudentsSayingAsyncTask(int id);

        #endregion
    }
}