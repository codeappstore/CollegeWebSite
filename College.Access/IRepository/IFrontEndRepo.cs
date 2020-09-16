using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.AcademicItemsDto;
using College.Model.DataTransferObject.AcademicsDto;
using College.Model.DataTransferObject.AttachmentDto;
using College.Model.DataTransferObject.CarouselDto;
using College.Model.DataTransferObject.PageDto;
using College.Model.DataTransferObject.PopupDto;
using College.Model.DataTransferObject.TeacherDto;

namespace College.Access.IRepository
{
    public interface IFrontEndRepo
    {
        // Saves the current context 
        Task<bool> SaveChangesAsyncTask();

        #region Page Model

        //  Page list
        // Task<IList<PageModelDto>> FetchPageDataListAsyncTask();

        //  Page by id
        Task<PageModelDto> FetchPageDataByIdAsyncTask(int id);

        //  Page Create
        // Task<bool> CreatePageDataAsyncTask(PageModelDto page);

        //  Page Update
        Task<bool> UpdatePageDataAsyncTask(PageModelDto page);

        //  Page Delete
        // Task<bool> DeletePageDataAsyncTask(int id);

        #endregion

        #region Carousel Model

        //  Carousel list
        Task<IList<CarouselModelDto>> FetchCarouselListAsyncTask();

        //  Carousel by id
        Task<CarouselModelDto> FetchCarouselByIdAsyncTask(int id);

        //  Carousel Create
        Task<bool> CreateCarouselAsyncTask(CarouselModelDto carousel);

        //  Carousel Update
        Task<bool> UpdateCarouselAsyncTask(CarouselModelDto carousel);

        //  Carousel Delete
        Task<bool> DeleteCarouselAsyncTask(int id);

        #endregion

        #region Academic Model

        //  Academic list
        //Task<IList<AcademicModelDto>> FetchAcademicDataListAsyncTask();

        //  Academic by id
        Task<AcademicModelDto> FetchAcademicDataByIdAsyncTask(int id);

        //  Academic Update
        Task<bool> UpdateAcademicDataAsyncTask(AcademicModelDto academic);

        #endregion

        #region Academic Items

        //  Academic Items list
        Task<IList<AcademicItemsModelDto>> FetchAcademicItemListAsyncTask();

        //  Academic Items by id
        Task<AcademicItemsModelDto> FetchAcademicItemByIdAsyncTask(int id);

        //  Academic Items Create
        //Task<bool> CreateAcademicItemAsyncTask(AcademicItemsModelDto academic);

        //  Academic Items Update
        Task<bool> UpdateAcademicItemAsyncTask(AcademicItemsModelDto academic);

        //  Academic Items Delete
        // Task<bool> DeleteAcademicItemAsyncTask(int id);

        #endregion

        #region Teacher

        //  Teacher list
        Task<IList<TeacherModelDto>> FetchTeacherListAsyncTask();

        //  Teacher by id
        Task<TeacherModelDto> FetchTeacherByIdAsyncTask(int id);

        //  Teacher Create
        Task<bool> CreateTeacherAsyncTask(TeacherModelDto teacher);

        //  Teacher Update
        Task<bool> UpdateTeacherAsyncTask(TeacherModelDto teacher);

        //  Teacher Delete
        Task<bool> DeleteTeacherAsyncTask(int id);

        #endregion

        #region Popup

        //  Popup by id
        Task<PopupModelDto> FetchPopUpByIdAsyncTask(int id);

        //  Popup Update
        Task<bool> UpdatePopUpAsyncTask(PopupModelDto popup);

        #endregion

        #region Attachment

        //  Attachment by id
        Task<AttachmentModelDto> FetchAttachmentByIdAsyncTask(int id);

        //  Attachment Update
        Task<bool> UpdateAttachmentAsyncTask(AttachmentModelDto attachment);

        #endregion
    }
}