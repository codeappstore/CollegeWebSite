using System.Collections.Generic;
using College.Model.DataTransferObject.PageDto;

namespace College.Model.DataTransferObject.GalleryDto
{
    public class PageGalleryImageModelDto
    {
        public PageModelDto Page { get; set; }
        public IList<GalleryModelDto> Gallery { get; set; }
        public IList<ImageModelDto> Image { get; set; }
    }
}