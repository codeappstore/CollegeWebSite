using System.Collections.Generic;

namespace College.Model.DataTransferObject.GalleryDto
{
    public class GalleryImageModelDto
    {
        public GalleryModelDto Gallery { get; set; }
        public IList<ImageModelDto> Images { get; set; }
    }
}