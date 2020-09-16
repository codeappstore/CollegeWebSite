using System.Collections.Generic;
using College.Model.DataTransferObject.PageDto;

namespace College.Model.DataTransferObject.DownloadsDto
{
    public class DownloadsPageModelDto
    {
        public PageModelDto Page { get; set; }
        public IList<DownloadModelDto> Downloads { get; set; }
    }
}