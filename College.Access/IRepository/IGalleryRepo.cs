using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.GalleryDto;

namespace College.Access.IRepository
{
    public interface IGalleryRepo
    {
        /// <summary>
        ///     Fetch All Gallery
        /// </summary>
        /// <returns>IList&lt;GalleryModelDto&gt;</returns>
        Task<IList<GalleryModelDto>> FetchAllGalleryAsyncTask();

        /// <summary>
        ///     Update Gallery
        /// </summary>
        /// <param name="gallery"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateGalleryAsyncTask(GalleryModelDto gallery);

        /// <summary>
        ///     Create Gallery
        /// </summary>
        /// <param name="gallery"></param>
        /// <returns>bool</returns>
        Task<bool> CreateGalleryAsyncTask(GalleryModelDto gallery);

        /// <summary>
        ///     Fetch Gallery
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GalleryModelDto</returns>
        Task<GalleryModelDto> FetchGalleryAsyncTask(int id);

        /// <summary>
        ///     Delete Gallery
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteGalleryAsyncTask(int id);

        /// <summary>
        ///     Fetch All Image
        /// </summary>
        /// <returns>IList&lt;ImageModelDto&gt;</returns>
        Task<IList<ImageModelDto>> FetchAllImageAsyncTask();

        /// <summary>
        ///     Fetch All Images of Specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IList<ImageModelDto>> FetchAllImageByGalleryIdAsyncTask(int id);

        /// <summary>
        ///     Update Image
        /// </summary>
        /// <param name="image"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateImageAsyncTask(ImageModelDto image);

        /// <summary>
        ///     Create Image
        /// </summary>
        /// <param name="image"></param>
        /// <returns>bool</returns>
        Task<bool> CreateImageAsyncTask(ImageModelDto image);

        /// <summary>
        ///     Fetch Image
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ImageModelDto</returns>
        Task<ImageModelDto> FetchImageAsyncTask(int id);

        /// <summary>
        ///     Delete Image
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteImageAsyncTask(int id);
    }
}