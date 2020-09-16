using System.Collections.Generic;
using System.Threading.Tasks;
using College.Model.DataTransferObject.DownloadsDto;

namespace College.Access.IRepository
{
    public interface IDownloadsRepo
    {
        /// <summary>
        ///     Fetch All Downloads
        /// </summary>
        /// <returns>IList&lt;DownloadModelDto&gt;</returns>
        Task<IList<DownloadModelDto>> FetchAllDownloadsAsyncTask();

        /// <summary>
        ///     Update Download
        /// </summary>
        /// <param name="download"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateDownloadAsyncTask(DownloadModelDto download);

        /// <summary>
        ///     Create Download
        /// </summary>
        /// <param name="download"></param>
        /// <returns>bool</returns>
        Task<bool> CreateDownloadAsyncTask(DownloadModelDto download);

        /// <summary>
        ///     Fetch Downloads
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DownloadModelDto</returns>
        Task<DownloadModelDto> FetchDownloadAsyncTask(int id);

        /// <summary>
        ///     Delete Downloads
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteDownloadAsyncTask(int id);
    }
}